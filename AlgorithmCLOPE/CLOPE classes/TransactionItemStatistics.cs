using System.Collections;
using System.Collections.Generic;

namespace AlgorithmCLOPE.CLOPE_classes
{
    //Класс коллекции статистических данных о транзакциях
    public class TransactionItemStatistics : IList<TransactionItemStatistic>
    {
        //Fields
        private List<TransactionItemStatistic> statisticList;
        private Cluster myClaster;

        //Constructor
        public TransactionItemStatistics(Cluster owner)
        {
            statisticList = new List<TransactionItemStatistic>();
            myClaster = owner;
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
            myClaster.UpdateCluster();
        }
    }
}
