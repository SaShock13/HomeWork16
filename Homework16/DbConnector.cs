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
        SqlDataAdapter sqlDA;
        public OleDbDataAdapter oleDataAdapter;
        public DataTable sqlDataTable;
        public DataTable oleDataTable;
        DataRowView dataRowView;
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

            try
            {
                mySQLConnection = new SqlConnection(strBuilderConString.ConnectionString);
                //var sql = @"INSERT INTO Customerz ([LastName],[FirstName],[SurName],[Email],[Phone]) VALUES 
                //(N'Смирнова', N'Екатерина', N'Алексеевна', 'ekaterina_smirnova @example.com',792345),
                //(N'Кузнецов', N'Максим', N'Андреевич', 'maxim_kuznetsov @example.com',793456),
                //(N'Попова', N'Ольга', N'Дмитриевна', 'olga_popova @example.com',794567),
                //(N'Васильев', N'Иван', N'Егорович', 'ivan_vasilyev @example.com',795678)
                //";


                var sql = "select * from Customerz";

                //SqlCommand command = new SqlCommand(sql, mySQLConnection);

                //mySQLConnection.Open();
                //MessageBox.Show(mySQLConnection.State.ToString());
                //command.ExecuteNonQuery();
                //mySQLConnection.Close();


                sqlDataTable = new DataTable();
                sqlDA = new SqlDataAdapter();


                sql = @"SELECT * FROM Customerz Order By Email";
                sqlDA.SelectCommand = new SqlCommand(sql, mySQLConnection);

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
            var oleConnection = new OleDbConnection(oleStrBuilder.ConnectionString);

            var sql = "";

            sql = "select * from Purchases";
            oleDataAdapter.SelectCommand = new OleDbCommand(sql, oleConnection);

            sql = "DELETE FROM Purchases WHERE Id = @id";
            oleDataAdapter.DeleteCommand = new OleDbCommand(sql, oleConnection);
            oleDataAdapter.DeleteCommand.Parameters.Add("@id", OleDbType.Integer, 4, "id");




            try
            {

                #region запросы
                //sql = "DROP TABLE Purchases";
                //sql = @"CREATE TABLE Purchases (Id INT IDENTITY PRIMARY KEY,Email VARCHAR(30) not null,Name VARCHAR(50) not null,Code INT not null)";
                //sql = "INSERT INTO Purchases (Email, Name, Code) VALUES ('ivan_vasilyev @example.com', 'Холодильник ''Морозко''',123456)";

                //sql = "INSERT INTO Purchases (Email, Name, Code) VALUES ('ivan_vasilyev@example.com', 'Холодильник ''Морозко''',123456)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('alex_ivanov@example.com', 'Стиральная машина ''Белоснежка''', 234567)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('ekaterina_smirnova@example.com', 'Пылесос ''Чистый дом''', 345678)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('maxim_kuznetsov@example.com', 'Микроволновая печь ''Мгновенный обед''', 456789)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('olga_popova@example.com', 'Телевизор ''Кристалл''', 567890)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('ekaterina_smirnova@example.com', 'Кофемашина ''Арома''', 678901)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('maxim_kuznetsov@example.com', 'Посудомоечная машина ''Блестящая''', 789012)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('alex_ivanov@example.com', 'Электрическая плита ''Гастроном''', 890123)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('anna_petrova@example.com', 'Камин ''Теплая атмосфера''', 901234)";
                //sql = "INSERT INTO Purchases(Email, Name, Code) VALUES('artem_sokolov@example.com', 'Увлажнитель воздуха ''Оазис''', 012345)";

                //sql = @"INSERT INTO Purchases (Email, Name, Code) VALUES
                //SELECT* FROM(SELECT 'ivan_vasilyev @example.com', 'Холодильник ''Морозко''',123456
                //UNION ALL SELECT 'alex_ivanov @example.com', 'Стиральная машина ''Белоснежка''', 234567
                //UNION ALL SELECT 'ekaterina_smirnova @example.com', 'Пылесос ''Чистый дом''', 345678
                //UNION ALL SELECT 'maxim_kuznetsov @example.com', 'Микроволновая печь ''Мгновенный обед''', 456789); ";
                #endregion

                //sql = "Select * from Purchases";
                //OleDbCommand command = new OleDbCommand(sql, oleConnection);

                //oleConnection.Open();
                //command.ExecuteNonQuery();
                //MessageBox.Show(oleConnection.State.ToString());
                //oleConnection.Close();

                
                oleDataAdapter.Fill(oleDataTable);

            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }


            
            //mySQLConnection = new SqlConnection(strBuilderConString.ConnectionString);

            //sqlDataTable = new DataTable();
            //sqlDA = new SqlDataAdapter();


            //string sql = @"SELECT * FROM [Customerz] Order By [Email]";
            //sqlDA.SelectCommand = new SqlCommand(sql, mySQLConnection);

            //sqlDA.Fill(sqlDataTable);


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
    }
}
