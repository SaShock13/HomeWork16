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

    #region Товары
    //    Код: 123456, Название: Холодильник "Морозко"
    //2. Код: 234567, Название: Стиральная машина "Белоснежка"
    //3. Код: 345678, Название: Пылесос "Чистый дом"
    //4. Код: 456789, Название: Микроволновая печь "Мгновенный обед"
    //5. Код: 567890, Название: Телевизор "Кристалл"
    //6. Код: 678901, Название: Кофемашина "Арома"
    //7. Код: 789012, Название: Посудомоечная машина "Блестящая"
    //8. Код: 890123, Название: Электрическая плита "Гастроном"
    //9. Код: 901234, Название: Камин "Теплая атмосфера"
    //10. Код: 012345, Название: Увлажнитель воздуха "Оазис"
    //11. Код: 432109, Название: Фен "Стрижка ветра"
    //12. Код: 543210, Название: Утюг "Гладкие складки"
    //13. Код: 654321, Название: Кухонный комбайн "Все в одном"
    //14. Код: 765432, Название: Блендер "Нежные коктейли"
    //15. Код: 876543, Название: Мясорубка "Фарш мечты"
    //16. Код: 987654, Название: Чайник "Электробур"
    //17. Код: 098765, Название: Вакуумный упаковщик "Свежесть"
    //18. Код: 109876, Название: Мультиварка "Кулинарные эксперименты"
    //19. Код: 210987, Название: Барный холодильник "Ледяной вихрь"
    //20. Код: 321098, Название: Швейная машинка "Творческий порыв"
    #endregion
    #region Покупатели
    //1. Имя: Александр, Фамилия: Иванов, Отчество: Петрович, Телефон: +79123456789, Email: alex_ivanov @example.com
    //2. Имя: Екатерина, Фамилия: Смирнова, Отчество: Алексеевна, Телефон: +79234567890, Email: ekaterina_smirnova @example.com
    //3. Имя: Максим, Фамилия: Кузнецов, Отчество: Андреевич, Телефон: +79345678901, Email: maxim_kuznetsov @example.com
    //4. Имя: Ольга, Фамилия: Попова, Отчество: Дмитриевна, Телефон: +79456789012, Email: olga_popova @example.com
    //5. Имя: Иван, Фамилия: Васильев, Отчество: Егорович, Телефон: +79567890123, Email: ivan_vasilyev @example.com
    //6. Имя: Анна, Фамилия: Петрова, Отчество: Александровна, Телефон: +79678901234, Email: anna_petrova @example.com
    //7. Имя: Артем, Фамилия: Соколов, Отчество: Максимович, Телефон: +79789012345, Email: artem_sokolov @example.com
    //8. Имя: Мария, Фамилия: Михайлова, Отчество: Егоровна, Телефон: +79890123456, Email: maria_mikhailova @example.com
    //9. Имя: Андрей, Фамилия: Новиков, Отчество: Иванович, Телефон: +79901234567, Email: andrey_novikov @example.com
    //10. Имя: Елена, Фамилия: Федорова, Отчество: Николаевна, Телефон: +70000000000, Email: elena_fedorova @example.com
    //11. Имя: Дмитрий, Фамилия: Егоров, Отчество: Сергеевич, Телефон: +70111111111, Email: dmitry_egorov @example.com
    //12. Имя: Светлана, Фамилия: Волкова, Отчество: Александровна, Телефон: +70222222222, Email: svetlana_volkova @example.com
    //13. Имя: Павел, Фамилия: Козлов, Отчество: Никитич, Телефон: +70333333333, Email: pavel_kozlov @example.com
    //14. Имя: Наталья, Фамилия: Лебедева, Отчество: Викторовна, Телефон: +70444444444, Email: natalya_lebedeva @example.com
    //15. Имя: Алексей, Фамилия: Семенов, Отчество: Юрьевич, Телефон: +70555555555, Email: alexey_semenov @example.com
    //16. Имя: Юлия, Фамилия: Ефимова, Отчество: Валентиновна, Телефон: +70666666666, Email: yuliya_efimova @example.com
    //17. Имя: Владимир, Фамилия: Денисов, Отчество: Олегович, Телефон: +70777777777, Email: vladimir_denisov @example.com
    //18. Имя: Евгения, Фамилия: Романова, Отчество: Владимировна, Телефон: +70888888888, Email: evgenia_romanova @example.com
    //19. Имя: Роман, Фамилия: Зайцев, Отчество: Анатольевич, Телефон: +70999999999, Email: roman_zaitsev @example.com
    //20. Имя: Людмила, Фамилия: Комарова, Отчество: Федоровна, Телефон: +71010101010, Email: lyudmila_komarova @example.com
    #endregion


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DbConnector connector = new DbConnector();
        public MainWindow()
        {
            InitializeComponent();


            

            if (connector.sqlDataTable!=null)
            {
            dgSQL.ItemsSource = connector.sqlDataTable.DefaultView;
            
            }
            if (connector.oleDataTable != null)
            {
                dgAccess.ItemsSource = connector.oleDataTable.DefaultView;

            }
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAddCustomerClick(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAll_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemAddClick(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemDeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void addCustomerBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddPurchaseBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void allPurchaseBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DelAllPurchaseBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DelPurchaseBtnClick(object sender, RoutedEventArgs e)
        {
            //connector.oleDataAdapter.DeleteCommand.ExecuteNonQuery();
            DataRowView dataRowView;
            dataRowView = (DataRowView)dgAccess.SelectedItem;
            dataRowView.Row.Delete();
            connector.AccessUpdate();
        }
    }
}
