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
    /// Interaction logic for AddWindow2.xaml
    /// </summary>
    public partial class AddWindow2 : Window
    {
        private string daypointer;
        private MainWindow pointer;
        public AddWindow2(in string daypointer_, in MainWindow pointer_)
        {
            daypointer = daypointer_;
            pointer = pointer_;
            InitializeComponent();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string time = Time.Text;
            string dotime = Do.Text;
            string sobitie = Sobitie.Text;
            string place = Place.Text;
            App.connection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO \"Table1\" (Id_, Время, До, Событие, Место) VALUES (" + daypointer + ", \'" + time + "\', \'" + dotime + "\', N\'"+ sobitie + "\', N\'" + place + "\'" + ")", App.connection);
            command.ExecuteNonQuery();
            command = new SqlCommand("SELECT Id, Время, До, Событие, Место FROM Table1 WHERE(Id_ = " + daypointer + ")", App.connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            pointer.DateDataGrid.ItemsSource = null;
            pointer.DateDataGrid.ItemsSource = dt.DefaultView;
            pointer.DateDataGrid.Columns[2].Width = 100;
            pointer.DateDataGrid.Columns[3].Width = 100;
            App.connection.Close();
        }
    }
}
