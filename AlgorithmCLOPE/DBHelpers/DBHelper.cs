using System;
using System.Data.Common;

namespace AlgorithmCLOPE.DBHelpers
{
    public class DBHelper
    {

        public static DbConnection CreateDbConnection(string providerName, string connectionString)
        {
            // Assume failure.
            DbConnection connection = null;

            // Create the DbProviderFactory and DbConnection.
            if (!String.IsNullOrWhiteSpace(connectionString))
            {
                DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);

                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
            }
            // Return the connection.
            return connection;
        }

        public static DbDataAdapter CreateDataAdapter(string providerName, string connectionString)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            DbConnection connection = CreateDbConnection(providerName, connectionString);

            // Create the DbDataAdapter.
            DbDataAdapter adapter = factory.CreateDataAdapter();

            return adapter;
        }

        static DbDataAdapter CreateDataAdapterWithSelectCommand(DbConnection connection, string tablename)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection);

            string queryString = $"SELECT * FROM {tablename}";

            // Create the DbCommand.
            DbCommand command = factory.CreateCommand();
            command.CommandText = queryString;
            command.Connection = connection;

            // Create the DbDataAdapter.
            DbDataAdapter adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = command;

            return adapter;
        }
    
        public static DbCommand GetInsertCommand(out DbDataAdapter adapter, 
                                                 DbConnection connection, 
                                                 string tablename)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(connection);

            adapter = CreateDataAdapterWithSelectCommand(connection, tablename);

            DbCommandBuilder builder = factory.CreateCommandBuilder();
            builder.QuotePrefix = "[";
            builder.QuoteSuffix = "]";
            builder.DataAdapter = adapter;

            connection.Open();
            adapter.SelectCommand.ExecuteNonQuery();
            connection.Close();

            DbCommand insertCommand = builder.GetInsertCommand();

            return insertCommand;
        }
    }
}
