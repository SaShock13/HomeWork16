using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Homework16


    
{
    #region TODO
    //todo: Разработайте приложение, в котором будет подключено два разных источника данных: MSSQLLocalDB и MS Access.
    //todo: Организуйте подключение, выведите строку подключения и статус подключения к этим источникам данных.
    //todo: Вывод данных выполните в графическом интерфейсе.Также необходимо учесть, что должна быть авторизация по логину и паролю для источника данных.

    //todo:    Расширим программу из задания 1. Предположим, что в разных источниках данных храниться информация из
    //двух систем, информацию из них необходимо сверять между собой, чтобы находить и выводить недостающую.Создайте и
    //заполните таблицы произвольными данными для решения задачи.Первый источник данных должен содержать таблицу с
    //полями:
    //ID
    //Фамилия
    //Имя
    //Отчество
    //Номер телефона (может быть пустым)
    //Email.
    //Второй источник данных содержит таблицу со следующими полями:
    //ID
    //Email
    //Код товара
    //Наименование товара
    //У нас две разные системы, которые хранят разную информацию по клиентам.Одно из полей должно быть
    //идентификатором.В нашем случае — это поле e-mail.

    //todo: Расширим программу из задания 2. Создайте запросы SQL:
    //Select — для выборки данных о покупках по клиенту.
    //Insert — вставка во вторую таблицу по совершенной покупке, а также добавление новых клиентов в первую таблицу.
    //Update — обновление данных по клиенту из первой таблицы.
    //Delete — очистка таблиц.
    //После чего, используя запросы SQL и компоненты WPF, разработайте программу для сотрудников магазина.
    #endregion

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DbConnector connector = new DbConnector();
        DataRow customerDataRow;
        public MainWindow()
        {
            InitializeComponent();
            customerDataRow = connector.sqlDataTable.NewRow();
            if (connector.sqlDataTable!=null)
            {
                dgSQL.DataContext = connector.sqlDataTable.DefaultView;
            }
            if (connector.oleDataTable != null)
            {
                dgAccess.DataContext = connector.oleDataTable.DefaultView;
            }
        }
        private void addCustomerBtnClick(object sender, RoutedEventArgs e)
        {
            bool isExist = false;
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.ShowDialog();
            if (addCustomerWindow.DialogResult == true)
            {
                customerDataRow = connector.sqlDataTable.NewRow();
                customerDataRow["LastName"]= addCustomerWindow.tbLastName.Text;
                customerDataRow["SurName"] = addCustomerWindow.tbFirstName.Text;
                customerDataRow["FirstName"] = addCustomerWindow.tbSurName.Text;
                customerDataRow["Phone"] = addCustomerWindow.tbPhone.Text;
                customerDataRow["Email"] = addCustomerWindow.tbEmail.Text;

                foreach (DataRow row in connector.sqlDataTable.Rows)
                {
                    if (row["Email"].ToString() == addCustomerWindow.tbEmail.Text)
                    {
                        MessageBox.Show("Такой email уже существует в базе!!!");
                        isExist = true;
                        break;
                    }
                }
                if (isExist == false)
                {
                    connector.sqlDataTable.Rows.Add(customerDataRow);
                }
                connector.SqlUpdate();
            }
        }
        private void AddPurchaseBtnClick(object sender, RoutedEventArgs e)
        {
            DataRowView customerDataRowView;
            DataRow purchaseDataRow = connector.oleDataTable.NewRow();
            customerDataRowView = dgSQL.SelectedItem as DataRowView;
            AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow(purchaseDataRow);
            if (dgSQL.SelectedIndex!=-1)
            {
                addPurchaseWindow.ShowDialog();
                if (addPurchaseWindow.DialogResult == true)
                {
                    purchaseDataRow = addPurchaseWindow.purchaseRow;
                    purchaseDataRow["Email"] = customerDataRowView["Email"];
                    connector.oleDataTable.Rows.Add(purchaseDataRow); //Добавляет покупку в DataTable
                    connector.AccessUpdate();
                    connector.oleDataTable = new DataTable();
                    connector.oleDataAdapter.Fill(connector.oleDataTable);
                    dgAccess.ItemsSource = connector.oleDataTable.DefaultView;

                }
            }
        }

        private void allPurchaseBtnClick(object sender, RoutedEventArgs e)
        {
            connector.ShowAllPurchases();
        }

        private void DelAllPurchaseBtnClick(object sender, RoutedEventArgs e)
        {
            if (dgSQL.SelectedIndex != -1)
            {
            connector.DeleteAllCustomersPurchases((dgSQL.SelectedItem as DataRowView)["Email"].ToString());

            }
        }

        private void DelPurchaseBtnClick(object sender, RoutedEventArgs e)
        {

            if (dgAccess.SelectedIndex != -1)
            {
                DataRowView dataRowView;
                dataRowView = (DataRowView)dgAccess.SelectedItem;
                dataRowView.Row.Delete();
                connector.AccessUpdate(); 
            }

        }
        private void UpdateCustomerBtnClick(object sender, RoutedEventArgs e)
        {
            if (dgSQL.SelectedIndex!=-1)
            {
                customerDataRow = connector.sqlDataTable.NewRow();
                customerDataRow = dgSQL.SelectedItem as DataRow;
                DataRowView dataRow = (DataRowView)dgSQL.SelectedItem;
                UpdateCustomerWindow updateCustomerWindow = new UpdateCustomerWindow(dataRow);
                updateCustomerWindow.ShowDialog();

                dataRow["LastName"] = updateCustomerWindow.tbLastName.Text;
                dataRow["SurName"] = updateCustomerWindow.tbSurName.Text;
                dataRow["FirstName"] = updateCustomerWindow.tbFirstName.Text;
                dataRow["Phone"] = updateCustomerWindow.tbPhone.Text;
                dataRow["Email"] = updateCustomerWindow.tbEmail.Text;

                connector.SqlUpdate(); 
            }
        }

        private void dgSQL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRowView;
            if (dgSQL.SelectedIndex == -1 )
            {
                dgSQL.SelectedIndex = 0;
            }
            dataRowView = (DataRowView)dgSQL.SelectedItem;

            
            connector.ShowPurchasesOfCustomer(dataRowView["Email"].ToString());
            dgAccess.SelectedIndex = 0;
        }

        private void DelCustomerBtnClick(object sender, RoutedEventArgs e)
        {
            if (dgSQL.SelectedIndex!=-1)
            {
                DataRowView dataRowView = (DataRowView)dgSQL.SelectedItem;
                connector.DeleteAllCustomersPurchases((dgSQL.SelectedItem as DataRowView)["Email"].ToString());
                dataRowView.Row.Delete();
                dgSQL.SelectedIndex = 0;
                connector.SqlUpdate(); 
            }
            
        }
    }
}
