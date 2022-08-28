using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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

        private DbConnection CLOPEDataBaseConnection;
        private string CLOPEtableName = "Mushrooms";

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Настроим соединение с базой данных
            string parameterName = "AlgorithmCLOPE.Properties.Settings.CLOPEDBConnectionString";
            string providerName = ConfigurationManager.ConnectionStrings[parameterName].ProviderName;
            string connectionStr = ConfigurationManager.ConnectionStrings[parameterName].ConnectionString;
            CLOPEDataBaseConnection = dbHlp.CreateDbConnection(providerName, connectionStr); // new SqlConnection(connectionStr);
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
                        string[] fragments = line.Split(new char[] {','});
                        if (fragments != null && fragments.Count() > 0)
                        {
                            // Наполним insert-команду данными для записи
                            for (int i=0; i < fragments.Length & i < insertCmd.Parameters.Count-1; i++)
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

        private void btnInitClusters_Click(object sender, EventArgs e)
        {

        }
    }
}
