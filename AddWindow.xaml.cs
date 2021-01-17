using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
namespace Datebook
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private MainWindow pointer;
        public AddWindow(in MainWindow pointer_)
        {
            pointer = pointer_;
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string day = Day.Text;
            string month = Month.Text;
            string year = Year.Text;
            App.connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO \"Table\" (День, Месяц, Год) VALUES ("+ '\'' + day + '\'' + ", N\'" + month + "\', \'" + year + '\'' + ")", App.connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("SELECT * FROM \"Table\"", App.connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            pointer.tableDataGrid.ItemsSource = null;
            pointer.tableDataGrid.ItemsSource = dt.DefaultView;
            App.connection.Close();
        }
    }
}
