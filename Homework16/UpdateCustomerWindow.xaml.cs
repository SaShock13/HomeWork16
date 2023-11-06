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
using System.Windows.Shapes;

namespace Homework16
{
    /// <summary>
    /// Логика взаимодействия для UpdateCustomerWindow.xaml
    /// </summary>
    public partial class UpdateCustomerWindow : Window
    {
        DataRowView inputDataRow;
        public UpdateCustomerWindow(DataRowView inputDataRow)
        {
            InitializeComponent();
            this.inputDataRow = inputDataRow;
            tbSurName.Text = inputDataRow["SurName"].ToString();
            tbFirstName.Text = inputDataRow["FirstName"].ToString();
            tbLastName.Text = inputDataRow["LastName"].ToString();
            tbPhone.Text = inputDataRow["Phone"].ToString();
            tbEmail.Text = inputDataRow["Email"].ToString();

        }
        private void btnOk(object sender, RoutedEventArgs e)
        {
            if (tbEmail != null & tbLastName != null & tbPhone != null)
            {
                DialogResult = true;
            }

        }
    }
}
