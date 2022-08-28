using AlgorithmCLOPE.CLOPE_classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AlgorithmCLOPE
{
    /// <summary>
    /// Описывает любую транзакцию в базе данных
    /// </summary>
    public class Transaction: IEquatable<Transaction>
    {
        public List<TransactionItem> Items { get; set; }

        //Constructor
        public Transaction()
        {
            Items = new List<TransactionItem>();
        }

        //Constructor
        public Transaction(IEnumerable<TransactionItem> items)
        {
            Items = items.ToList();
        }

        //Metods
        public bool Equals(Transaction other)
        {
            bool result = false;
            if (this.Items.Count == other.Items.Count)
            {
                result = true;
                //Транзакции равны если содержат одинаковые наборы элементов
                for (int i=0; i < this.Items.Count; i++)
                {
                    if (!this.Items[i].Equals(other.Items[i]))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        public static Transaction Parse(IDataRecord record)
        {
            Transaction newTransaction = new Transaction();

            for (int i=0; i < record.FieldCount; i++)
            {
                newTransaction.Items.Add(new TransactionItem(record[i]));
            }

            return newTransaction;
        }
    }
}
