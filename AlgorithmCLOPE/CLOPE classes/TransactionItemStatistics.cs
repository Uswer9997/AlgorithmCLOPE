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

        #region IList Interface

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
                int index = statisticList.IndexOf(item); //найдём индекс подобного элемента
                statisticList[index].Increase(item); //увеличим число вхождений на величину в item
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

        #endregion

        //Metods
        /// <summary>
        /// Обновляет показатели кластера
        /// </summary>
        private void onClasterUpdate()
        {
            myCluster.UpdateCluster();
        }

        /// <summary>
        /// Выполняет поиск объекта статистики, у которого базис равен параметру searchItem
        /// и возвращает первое найденное вхождение в пределах всего списка.
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns>Возвращает найденный объект статистики. Если объект не найден возвращает null</returns>
        public TransactionItemStatistic Find(TransactionItem searchItem)
        {
            foreach (TransactionItemStatistic currentItem in this)
            {
                if (currentItem.Basis.Equals(searchItem))
                {
                    return currentItem;
                }
            }
            return null;
        }
    }
}
