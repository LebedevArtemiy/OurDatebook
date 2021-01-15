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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Datebook
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddWindow().Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Datebook.DatabaseDataSet databaseDataSet = ((Datebook.DatabaseDataSet)(this.FindResource("databaseDataSet")));
            // Load data into the table Table. You can modify this code as needed.
            Datebook.DatabaseDataSetTableAdapters.TableTableAdapter databaseDataSetTableTableAdapter = new Datebook.DatabaseDataSetTableAdapters.TableTableAdapter();
            databaseDataSetTableTableAdapter.Fill(databaseDataSet.Table);
            System.Windows.Data.CollectionViewSource tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tableViewSource")));
            tableViewSource.View.MoveCurrentToFirst();
        }
        private void tabledataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            App.connection.Open();
            var ci = new DataGridCellInfo(tableDataGrid.CurrentItem, tableDataGrid.Columns[0]);
            var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
            string id_db = content.Text;
            //MessageBox.Show("SELECT Время, До, Событие, Место FROM Table1 WHERE(Id = " + id_db + ")");
            SqlCommand command = new SqlCommand("SELECT Время, До, Событие, Место FROM Table1 WHERE(Id = " + id_db + ")", App.connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DateDataGrid.ItemsSource = dt.DefaultView;
            DateDataGrid.Columns[2].Width = 100;
            DateDataGrid.Columns[3].Width = 100;
            App.connection.Close();
        }
    }
}
