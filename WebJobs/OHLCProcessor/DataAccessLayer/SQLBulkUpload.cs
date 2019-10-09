using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccessLayer
{
    public class SQLBulkUpload<T>
    {
        public IList<T> InternalStore { get; set; }
        public string TableName { get; set; }
        public int CommitBatchSize { get; set; } = 1000;
        public string ConnectionString { get; set; }

        public void Commit()
        {
            if (InternalStore.Count > 0)
            {
                DataTable dt;
                int numberOfPages = (InternalStore.Count / CommitBatchSize) + (InternalStore.Count % CommitBatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    dt = InternalStore.Skip(pageIndex * CommitBatchSize).Take(CommitBatchSize).ToDataTable();
                    BulkInsert(dt);
                }
            }
        }

        public void BulkInsert(DataTable dt)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // make sure to enable triggers
                // more on triggers in next post
                SqlBulkCopy bulkCopy =
                    new SqlBulkCopy
                    (
                    connection,
                    SqlBulkCopyOptions.TableLock |
                    SqlBulkCopyOptions.FireTriggers |
                    SqlBulkCopyOptions.UseInternalTransaction,
                    null
                    );

                // set the destination table name
                bulkCopy.DestinationTableName = TableName;
                connection.Open();

                // write the data in the "dataTable"
                bulkCopy.WriteToServer(dt);
                connection.Close();
            }
            // reset
            //this.dataTable.Clear();
        }

    }
}
