using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmCLOPE
{
    //Класс репозитория кластеров
    //Класс будет реализовывать автоматическое присвоение уникальных индексов для каждого кластера
    //Класс будет создан по шаблону singleton
    public class ClusterRepository
    {
        #region Singleton
        private static ClusterRepository repository; //ссылка на репозиторий

        //Реализация для шаблона Singleton
        public static ClusterRepository GetRepository()
        {
            if (repository == null)
            {
                ClusterRepository repo = new ClusterRepository();
                repository = repo;
                return repo;
            }
            else
            {
                return repository;
            }
        }
                       
        //Constructor
        private ClusterRepository()
        {
            clusterList = new List<Cluster>();
        }

        #endregion

        //Fields
        private static int uniqueIndex;

        private List<Cluster> clusterList;

        //Property
        public int Count { get => clusterList.Count; }

        //Metods
        //Реализуем только нужные методы для решения задачи
        public Cluster Add(Cluster cluster)
        {
            uniqueIndex++;
            cluster.Index = uniqueIndex;
clusterList.Add(cluster);
            return cluster;
        }

        public Cluster GetCluster(int index)
        {
            return clusterList[index];
        }

        public void Remove(int index)
        {
            for (int i=clusterList.Count-1; i >=0; i--)
            {
                if (clusterList[i].Index == index)
                {
                    clusterList.RemoveAt(i);
                }
            }
        }

        public List<Cluster> GetAll()
        {
            return clusterList;
        }

        public void Clear()
        {
            clusterList.Clear();
        }

    }
}
