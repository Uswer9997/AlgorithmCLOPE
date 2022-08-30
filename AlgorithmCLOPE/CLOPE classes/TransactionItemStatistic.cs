using System;

namespace AlgorithmCLOPE.CLOPE_classes
{
    /// <summary>
    /// Класс статистики вхождения элементов TransactionItem в кластер (Cluster)
    /// </summary>
    public class TransactionItemStatistic : IEquatable<TransactionItemStatistic>
    {
        //Properties
        public int Count { get; private set; }
        public TransactionItem Basis { get; private set; }

        //Constructor
        public TransactionItemStatistic(TransactionItem basis)
        {
            this.Basis = basis;
            this.Count = 1;
        }

        //Metods
        #region Методы увеличения статистики
        public bool Increase(TransactionItem item)
        {
            bool result = false;
            if (this.Basis.Equals(item))
            {
                this.Count += 1;
                result = true;
            }
            return result;
        }

        public bool Increase(TransactionItemStatistic statisticItem)
        {
            bool result = false;
            if (this.Equals(statisticItem))
            {
                this.Count += statisticItem.Count;
                result = true;
            }
            return result;
        }
        #endregion

        #region Методы уменьшения статистики
        public bool Decrease(TransactionItem item)
        {
            bool result = false;
            if (this.Basis.Equals(item))
            {
                this.Count -= 1;
                result = true;
            }
            return result;
        }

        public bool Decrease(TransactionItemStatistic statisticItem)
        {
            bool result = false;
            if (this.Equals(statisticItem))
            {
                this.Count -= statisticItem.Count;
                result = true;
            }
            return result;
        }

        public void Decrease(int value)
        {
            this.Count -= value;
        }
        #endregion

        public bool Equals(TransactionItemStatistic other)
        {
            return this.Basis == other.Basis;
        }
    }
}
