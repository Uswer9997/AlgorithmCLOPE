using AlgorithmCLOPE.CLOPE_classes;
using System.Linq;

namespace AlgorithmCLOPE
{
    public class Cluster
    {
        //Properties
        public double Width { get; private set; }
        public double Height { get; private set; }
        public TransactionItemStatistics Statistics { get; private set; }

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

        public void UpdateCluster()
        {
            this.Height = this.GetHeight();
            this.Width = this.GetWidth();
        }

    }
}
