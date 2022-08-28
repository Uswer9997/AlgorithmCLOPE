using System.Collections;
using System.Collections.Generic;

namespace AlgorithmCLOPE.CLOPE_classes
{
    //Класс коллекции статистических данных о транзакциях
    public class TransactionItemStatistics : IList<TransactionItemStatistic>
    {
        //Fields
        private List<TransactionItemStatistic> statisticList;
        private Cluster myCluster;

        //Constructor
        public TransactionItemStatistics(Cluster owner)
        {
            statisticList = new List<TransactionItemStatistic>();
            myCluster = owner;
        }

        //Interface realisation
        public TransactionItemStatistic this[int index]
        {
            get => statisticList[index];
            set
            {
                statisticList[index] = value;
                onClasterUpdate();
            }
        }

        public int Count => statisticList.Count;

        public bool IsReadOnly => false;

        public void Add(TransactionItemStatistic item)
        {
            //Если добавляется элемент подобный уже имеющемуся в коллекции
            if (statisticList.Contains(item))
            {
                int index = statisticList.IndexOf(item);
                statisticList[index].Increase(item); //увеличит число вхождений на величину в item
            }
            else
            {
                statisticList.Add(item);
            }
            onClasterUpdate();
        }

        public void Clear()
        {
            statisticList.Clear();
            onClasterUpdate();
        }

        public bool Contains(TransactionItemStatistic item)
        {
            return statisticList.Contains(item);
        }

        public void CopyTo(TransactionItemStatistic[] array, int arrayIndex)
        {
            statisticList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TransactionItemStatistic> GetEnumerator()
        {
            return statisticList.GetEnumerator();
        }

        public int IndexOf(TransactionItemStatistic item)
        {
            return statisticList.IndexOf(item);
        }

        public void Insert(int index, TransactionItemStatistic item)
        {
            statisticList.Insert(index, item);
            onClasterUpdate();
        }

        public bool Remove(TransactionItemStatistic item)
        {
            bool res = statisticList.Remove(item);
            onClasterUpdate();
            return res;
        }

        public void RemoveAt(int index)
        {
            statisticList.RemoveAt(index);
            onClasterUpdate();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return statisticList.GetEnumerator();
        }

        //Metods
        private void onClasterUpdate()
        {
            myCluster.UpdateCluster();
        }

        /// <summary>
        /// Ищет все объекты статистики, у которых базис равен параметру searchItem
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns>Возвращает список объектов статистики</returns>
        public List<TransactionItemStatistic> Find(TransactionItem searchItem)
        {
            List<TransactionItemStatistic> resultList = new List<TransactionItemStatistic>();
            foreach (TransactionItemStatistic currentItem in this)
            {
                if (currentItem.Basis.Equals(searchItem))
                {
                    resultList.Add(currentItem);
                }
            }
            return resultList;
        }
    }
}
