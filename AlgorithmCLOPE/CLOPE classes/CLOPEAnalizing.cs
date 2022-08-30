using System;
using System.Collections.Generic;

namespace AlgorithmCLOPE.CLOPE_classes
{
    class CLOPEAnalizing
    {
        //Fields
        public static double repulsion = 1;

        //Properties
        //public static List<Cluster> Clusters { get; set; }

        //Metods
        public static double DeltaAdd(Cluster cluster, Transaction transaction)
        {
            return DeltaAdd(cluster, transaction, CLOPEAnalizing.repulsion);
        }

        
        public static double DeltaAdd(Cluster cluster, Transaction transaction, double r)
        {
            double Snew = cluster.Height * cluster.Width + transaction.Items.Count;
            double Wnew = cluster.Width;
            //объект статистики для текущего элемента транзакции
            TransactionItemStatistic statisticItemForCurrentTransitionItem;
            for (int i=0; i < transaction.Items.Count; i++)
            {
                statisticItemForCurrentTransitionItem = cluster.Statistics.Find(transaction.Items[i]);
                if (statisticItemForCurrentTransitionItem == null )
                {
                    Wnew++;
                }
            }
            double arg1 = Snew * (cluster.TransactionCount + 1) / (Math.Pow(Wnew, r));
            double arg2 = cluster.Width == 0 ? 0 : (cluster.Height*cluster.Width* cluster.TransactionCount)/ Math.Pow(cluster.Width, r);

            return arg1 - arg2;
        }

    }
}
