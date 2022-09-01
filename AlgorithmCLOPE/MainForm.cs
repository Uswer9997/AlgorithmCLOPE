using AlgorithmCLOPE.CLOPE_classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Windows.Forms;
using dbHlp = AlgorithmCLOPE.DBHelpers.DBHelper;

namespace AlgorithmCLOPE
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //Fields
        private DbConnection CLOPEDataBaseConnection;
        private string CLOPEtableName = "Mushrooms"; //имя таблицы транзакций в базе
        private ClusterRepository clusterRepo; //репозиторий кластеров
        private bool IsRepulsionValid; //true, если пользователь ввёл корректный коэфф. отталкивания

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Настроим соединение с базой данных
            string parameterName = "AlgorithmCLOPE.Properties.Settings.CLOPEDBConnectionString";
            string providerName = ConfigurationManager.ConnectionStrings[parameterName].ProviderName;
            string connectionStr = ConfigurationManager.ConnectionStrings[parameterName].ConnectionString;
            CLOPEDataBaseConnection = dbHlp.CreateDbConnection(providerName, connectionStr);
            //Создадим репозиторий кластеров
            clusterRepo = ClusterRepository.GetRepository();
            //Инициализируем интерфейс программы
            RepulsionTextBox.Text = CLOPEAnalizing.repulsion.ToString();
        }


        /// <summary>
        /// Загрузим данные из файла в базу данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Data files|*.data";
            openFileDialog1.Multiselect = false;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFromFileToDB(openFileDialog1.FileName, CLOPEDataBaseConnection, CLOPEtableName);
            }
        }

        private void LoadFromFileToDB(string fileName, DbConnection connection, string tablename)
        {
            // Получим параметризованную команду вставки данных в базу,
            // а заодно и адаптер
            DbDataAdapter da;
            DbCommand insertCmd = dbHlp.GetInsertCommand(out da, connection, tablename);

            connection.Open();

            //Откроем поток файла на чтение
            using (System.IO.FileStream fsFile = new System.IO.FileStream(fileName,
                    System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                // Будем читать данные из файла построчно используя ридер
                using (System.IO.StreamReader fsStream = new System.IO.StreamReader(fsFile, System.Text.Encoding.UTF8))
                {
                    string line = fsStream.ReadLine();
                    while (line != null)
                    {
                        // Получим массив данных в строковом представлении
                        string[] fragments = line.Split(new char[] { ',' });
                        if (fragments != null && fragments.Count() > 0)
                        {
                            // Наполним insert-команду данными для записи
                            for (int i = 0; i < fragments.Length & i < insertCmd.Parameters.Count - 1; i++)
                            {
                                insertCmd.Parameters[i].Value = fragments[i];
                            }
                            insertCmd.Parameters["@P24"].Value = 0;
                            insertCmd.ExecuteNonQuery();
                        }
                        line = fsStream.ReadLine();
                    }
                }
            }

            connection.Close();
        }

        private void btnClusterize_Click(object sender, EventArgs e)
        {
            //Проверим, что данные для обработки существуют
            if (!DataPresents(CLOPEDataBaseConnection, CLOPEtableName))
            {
                MessageBox.Show("Не заполнена таблица для анализа.");
                return;
            }
            //Проверим, что коэффициент отталкивания введён верно
            if (!IsRepulsionValid)
            {
                MessageBox.Show("С такими данными мы ничего не посчитаем.");
                return;
            }
            //Погнали кластеризацию
            //Сначала инициализируем кластеры
            InitClusters();

            //*********** вспомогательный вывод ***************
            ClustersDataGridView.DataSource = clusterRepo.GetAll();


            //Сама кластеризация
            Clusterize();
        }

        private bool DataPresents(DbConnection connection, string tableName)
        {
            DbDataAdapter da = dbHlp.CreateDataAdapterWithSelectCommand(connection, tableName);
            da.SelectCommand.CommandText = $"SELECT COUNT(*) FROM {tableName}";
            int countRecs;
            try
            {
                connection.Open();
                if ((int.TryParse(da.SelectCommand.ExecuteScalar().ToString(), out countRecs)) && (countRecs > 0))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        private void RepulsionTextBox_Validated(object sender, EventArgs e)
        {
            double repulsion;
            if (double.TryParse(RepulsionTextBox.Text, out repulsion))
            {
                errorProvider1.Clear();
                CLOPEAnalizing.repulsion = repulsion;
                IsRepulsionValid = true;
            }
            else
            {
                IsRepulsionValid = false;
                errorProvider1.SetError(RepulsionTextBox, "Недопустимое значение");
            }
        }

        /// <summary>
        /// Инициализирует кластеры
        /// </summary>
        private void InitClusters()
        {
            clusterRepo.Clear(); //Удаляем все ранее созданные кластеры

            //Тут всё очевидно, создаём строку на выборку данных, открываем соединение
            string selectQuery = $"SELECT * FROM {CLOPEtableName}";
            CLOPEDataBaseConnection.Open();
            //Создаём ридер для чтения таблицы 
            DbDataReader reader = dbHlp.CreateCommand(CLOPEDataBaseConnection, selectQuery).ExecuteReader();
            Transaction currentTransaction; //текущая транзакция
            Cluster bestCluster; //кластер с максимальным приростом
            Cluster newCluster; //новый кластер
            double maxDelta; //максимальный прирост при добавлении для текущей транзакции
            double currentDelta; //прирост для текущей транзакции и текущего кластера
            int bestClusterIndex; //индекс кластера с максимумом прироста
            int transactionNumber = 0; //число обработанных транзакций для вывода на форму
            //Последовательно читаем таблицу транзакций
            while (reader.Read())
            {
                //создадим объект транзакции из считанных данных
                currentTransaction = Transaction.Parse((IDataRecord)reader);
                maxDelta = 0;

                newCluster = new Cluster();
                clusterRepo.Add(newCluster);//добавим пустой кластер

                bestClusterIndex = -1; //индекс кластера с максимумом прироста

                //найдём кластер с максимальным приростом
                for (int i = 0; i < clusterRepo.Count; i++)
                {
                    currentDelta = CLOPEAnalizing.DeltaAdd(clusterRepo.GetCluster(i), currentTransaction);
                    if (currentDelta > maxDelta)
                    {
                        maxDelta = currentDelta;
                        bestClusterIndex = i;
                    }
                }

                //получим кластер с максимальным приростом из репозитория
                bestCluster = clusterRepo.GetCluster(bestClusterIndex);

                if (bestCluster != newCluster)
                {
                    clusterRepo.Remove(newCluster);
                }

                //добавим в кластер информацию по всем объектам транзакции
                bestCluster.AddTransaction(currentTransaction);

                //***** визуализация процесса *******
                transactionNumber++;
                lblTransactions.Text = transactionNumber.ToString();
                Application.DoEvents();
            }
            reader.Close();
            CLOPEDataBaseConnection.Close();
        }

        private void Clusterize()
        {
            //string selectQuery = $"SELECT * FROM {CLOPEtableName}";
            //DbDataReader reader = dbHlp.CreateCommand(CLOPEDataBaseConnection,selectQuery).ExecuteReader();
            //Transaction currentTransaction;
            //while (reader.Read())
            //{
            //    currentTransaction = Transaction.Parse((IDataRecord)reader);

            //}
            //reader.Close();
        }
    }
}
