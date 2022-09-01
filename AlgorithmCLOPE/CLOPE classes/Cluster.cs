using AlgorithmCLOPE.CLOPE_classes;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmCLOPE
{
    public class Cluster
    {
        //Properties
        public double Width { get; private set; }
        public double Height { get; private set; }
        public int Square { get; private set; }
        public TransactionItemStatistics Statistics { get; private set; }
        public int TransactionCount { get; private set; }
        public int Index { get; set; }
        public bool IsEmpty { get => (Statistics.Count > 0) ? false : true; }

        //Constructor
        public Cluster()
        {
            this.Statistics = new TransactionItemStatistics(this);
        }

        //Metods
        private double GetHeight()
        {
            return (Statistics.Sum(s => s.Count) / Statistics.Count);
        }

        private double GetWidth()
        {
            return Statistics.Count;
        }

        private int GetSquare()
        {
            return (Statistics.Sum(s => s.Count) * Statistics.Count);
        }

        public void UpdateCluster()
        {
            this.Height = this.GetHeight();
            this.Width = this.GetWidth();
            this.Square = this.GetSquare();
        }

        /// <summary>
        /// Увеличивает статистику по элеметнам транзакции transaction
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        public void AddTransaction(Transaction transaction)
        {
            //статистика для текущего элемента транзакции
            TransactionItemStatistic currentItemStatistic;
            foreach (TransactionItem item in transaction.Items)
            {
                currentItemStatistic = new TransactionItemStatistic(item);
                this.Statistics.Add(currentItemStatistic);
            }
            TransactionCount++; //увеличим число псевдотранзакций в кластере
        }

        /// <summary>
        /// Уменьшает статистику по элеметнам транзакции transaction
        /// </summary>
        /// <param name="transaction">Транзакция</param>
        /// <returns>Возвращает результат операции: False если такого набора элементов уменьшить не возможно</returns>
        public bool RemoveTransaction(Transaction transaction)
        {
            //список объектов статистики, значения которых следует уменьшить
            var statisticItemList = new List<TransactionItemStatistic>();
            //статистика для текущего элемента транзакции
            TransactionItemStatistic currentItemStatistic;

            foreach (TransactionItem item in transaction.Items)
            {
                //ищем статистику для текущего элемента транзакции
                currentItemStatistic = this.Statistics.Find(item);
                //если для текущего элемента транзакции в кластере есть статистика
                if (currentItemStatistic != null)
                {
                    //помещаем элемент статистики во временный список
                    //То есть собираем эквивалент транзакции, но из элементов статистики
                    statisticItemList.Add(currentItemStatistic);
                }
                else
                {
                    return false; //рапортуем о провале операции
                }
            }

            //если список статистики удачно собран, то уменьшим показатели статистики
            foreach (TransactionItemStatistic item in statisticItemList)
            {
                item.Decrease(1); //уменьшаем статистику по текущему элементу
            }
            TransactionCount--; //уменьшим число псевдотранзакций в кластере
            return true;
        }
    }

}
