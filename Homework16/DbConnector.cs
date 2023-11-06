using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Data.OleDb;
using System.Windows;
using System.Xml.Linq;
using System.Windows.Controls.Primitives;

namespace Homework16
{
    internal class DbConnector
    {
        SqlConnection mySQLConnection;
        OleDbConnection oleConnection;
        SqlDataAdapter sqlDA;
        public OleDbDataAdapter oleDataAdapter;
        public DataTable sqlDataTable;
        public DataTable oleDataTable;

        
        public DbConnector()
        {
            MSSQLInitiation();
            AccessDBInitiation();
        }


        private void MSSQLInitiation()
        {

            var strBuilderConString = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "Homework16SQLDB"
            };
            mySQLConnection = new SqlConnection(strBuilderConString.ConnectionString);

            var sql = "SELECT * FROM Customerz";

            sqlDataTable = new DataTable();
            sqlDA = new SqlDataAdapter();


            sql = @"SELECT * FROM Customerz Order By Email";
            sqlDA.SelectCommand = new SqlCommand(sql, mySQLConnection);


            sql = @"INSERT INTO Customerz (LastName,  FirstName,  SurName,Phone,Email) 
                                 VALUES (@LastName, @FirstName, @SurName,@Phone,@Email); 
                     SET @id = @@IDENTITY;";

            sqlDA.InsertCommand = new SqlCommand(sql, mySQLConnection);

            sqlDA.InsertCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id").Direction = ParameterDirection.Output;
            sqlDA.InsertCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 20, "LastName");
            sqlDA.InsertCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            sqlDA.InsertCommand.Parameters.Add("@SurName", SqlDbType.NVarChar, 20, "SurName");
            sqlDA.InsertCommand.Parameters.Add("@Phone", SqlDbType.Int, 11, "Phone");
            sqlDA.InsertCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");




            sql = @"UPDATE Customerz SET 
                           LastName = @LastName,
                           FirstName = @FirstName,
                           SurName = @SurName,
                           Phone = @Phone, 
                           Email = @Email 

                    WHERE id = @id";

            sqlDA.UpdateCommand = new SqlCommand(sql, mySQLConnection);

            sqlDA.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 0, "id").SourceVersion = DataRowVersion.Original;

            sqlDA.UpdateCommand.Parameters.Add("@LastName", SqlDbType.NVarChar, 20, "LastName");
            sqlDA.UpdateCommand.Parameters.Add("@FirstName", SqlDbType.NVarChar, 20, "FirstName");
            sqlDA.UpdateCommand.Parameters.Add("@SurName", SqlDbType.NVarChar, 20, "SurName");
            sqlDA.UpdateCommand.Parameters.Add("@Phone", SqlDbType.Int, 11, "Phone");
            sqlDA.UpdateCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 50, "Email");

            sql = "DELETE FROM Customerz WHERE id = @id";

            sqlDA.DeleteCommand = new SqlCommand(sql, mySQLConnection);
            sqlDA.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "id");

            try
            {
                sqlDA.Fill(sqlDataTable);


            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message); 
            }


            


        }

        private void AccessDBInitiation()
        {
            oleDataTable = new DataTable();
            oleDataAdapter = new OleDbDataAdapter();
            
            var oleStrBuilder = new OleDbConnectionStringBuilder()
            {
                Provider = "Microsoft.ACE.OLEDB.12.0",

                DataSource = @"C:\Skillbox\MyHomeworks\Homework16\Homework16\Homework16AccessDB.accdb"
            };
            oleStrBuilder.Add("Jet OLEDB:Database Password","123456");
            oleConnection = new OleDbConnection(oleStrBuilder.ConnectionString);

            var sql = "";

            sql = "select * from Purchases";
            oleDataAdapter.SelectCommand = new OleDbCommand(sql, oleConnection);


            sql = "DELETE FROM Purchases WHERE Id = @id";
            oleDataAdapter.DeleteCommand = new OleDbCommand(sql, oleConnection);
            oleDataAdapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");

            sql = @"INSERT INTO Purchases (Email,Code,Name) 
                                 VALUES (@email,@Code,@Name);
                                 ";

            oleDataAdapter.InsertCommand = new OleDbCommand(sql, oleConnection);

            //oleDataAdapter.InsertCommand.Parameters.Add("@id", OleDbType.Integer, 4, "Id").Direction = ParameterDirection.Output; 
            oleDataAdapter.InsertCommand.Parameters.Add("@email", OleDbType.WChar, 50, "Email");
            oleDataAdapter.InsertCommand.Parameters.Add("@Code", OleDbType.WChar, 20, "Code");
            oleDataAdapter.InsertCommand.Parameters.Add("@Name", OleDbType.WChar, 50, "Name");
            //int insertedId = Convert.ToInt32(oleDataAdapter.InsertCommand.ExecuteScalar());







            try
            {
                oleDataAdapter.Fill(oleDataTable);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
            
            
        }
        public void AccessUpdate()
        {
            try
            {
                
                oleDataAdapter.Update(oleDataTable);
                
                    
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        public void ShowPurchasesOfCustomer(string email)
        {
            string sql = $"select * from Purchases where Email = @email";

            oleDataAdapter.SelectCommand = new OleDbCommand(sql, oleConnection);
            OleDbParameter parameter = new OleDbParameter("@email", email);
            oleDataAdapter.SelectCommand.Parameters.Add(parameter);
            oleDataTable.Rows.Clear();
            oleDataAdapter.Fill(oleDataTable);

            //sql = "DELETE FROM Purchases WHERE Id = @id";
            //oleDataAdapter.DeleteCommand = new OleDbCommand(sql, oleConnection);
            //oleDataAdapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");
        }

        public void ShowAllPurchases()
        {
            string sql = $"select * from Purchases order by Id";

            oleDataAdapter.SelectCommand = new OleDbCommand(sql, oleConnection);
           
            oleDataTable.Rows.Clear();
            oleDataAdapter.Fill(oleDataTable);

            //sql = "DELETE FROM Purchases WHERE Id = @id";
            //oleDataAdapter.DeleteCommand = new OleDbCommand(sql, oleConnection);
            //oleDataAdapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");
        }
        public void SqlUpdate()
        {
            try
            {

                sqlDA.Update(sqlDataTable);


            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }

        internal void DeleteAllCustomersPurchases(string email)
        {
            string sql = "delete * from Purchases where Email = @email";
            OleDbCommand delAllCommand = new OleDbCommand(sql,oleConnection);
            OleDbParameter emailParameter = new OleDbParameter("email", email);
            delAllCommand.Parameters.Add(emailParameter);
            oleConnection.Open();
            delAllCommand.ExecuteNonQuery();
            oleConnection.Close();
            oleDataTable.Rows.Clear();
            oleDataAdapter.Fill(oleDataTable);

        }
        public void DeleteCustomer(int id)
        {
           
        }

    }
}
