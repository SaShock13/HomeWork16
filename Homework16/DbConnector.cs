using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework16
{
    internal class DbConnector
    {
        SqlConnection mySQLConnection;
        SqlDataAdapter sqlDA;
        public DataTable sqlDataTable;
        DataRowView dataRowView;
        public DbConnector()
        {
            MSSQLInitiation();
        }


        private void MSSQLInitiation()
        {

            var strBuilderConString = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Homework16SQLDB"
            };

            mySQLConnection = new SqlConnection(strBuilderConString.ConnectionString);

            sqlDataTable = new DataTable();
            sqlDA = new SqlDataAdapter();

            
            string sql = @"SELECT * FROM [Customerz] Order By [Email]";
            sqlDA.SelectCommand = new SqlCommand(sql, mySQLConnection);

            sqlDA.Fill(sqlDataTable);


        }
    }
}
