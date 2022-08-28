using System;

namespace AlgorithmCLOPE.CLOPE_classes
{
    //Класс элемента транзакции
    //Экземпляр не хранит сам элемент, а хранит его хэш
    public class TransactionItem : IEquatable<TransactionItem>
    {
        private int objHash = 0;

        public TransactionItem(Object obj)
        {
            this.objHash = obj.GetHashCode();
        }

        public bool Equals(TransactionItem other)
        {
            return this.objHash == other.objHash;
        }
    }
}
