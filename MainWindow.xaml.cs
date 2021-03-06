﻿using System;
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
        private string id_db;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddWindow(this).Show();
         
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.connection.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM \"Table\"", App.connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            tableDataGrid.ItemsSource = dt.DefaultView;
            App.connection.Close();
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
            id_db = content.Text;
            //MessageBox.Show("SELECT Время, До, Событие, Место FROM Table1 WHERE(Id = " + id_db + ")");
            SqlCommand command = new SqlCommand("SELECT Id, Время, До, Событие, Место FROM Table1 WHERE(Id_ = " + id_db + ")", App.connection);
            SqlDataReader reader = command.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            DateDataGrid.ItemsSource = dt.DefaultView;
            DateDataGrid.Columns[2].Width = 100;
            DateDataGrid.Columns[3].Width = 100;
            App.connection.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            App.connection.Open();
            if (tableDataGrid.SelectedIndex > -1)
            {
                var ci = new DataGridCellInfo(tableDataGrid.Items[tableDataGrid.SelectedIndex], tableDataGrid.Columns[0]);
                var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
                string id_db = content.Text;
                SqlCommand command = new SqlCommand("DELETE FROM \"Table1\" WHERE (Id_ = " + id_db + ")", App.connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("DELETE FROM \"Table\" WHERE (Id = " + id_db + ")", App.connection);
                command.ExecuteNonQuery();
                command = new SqlCommand("SELECT * FROM \"Table\"", App.connection);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                tableDataGrid.ItemsSource = null;
                tableDataGrid.ItemsSource = dt.DefaultView;
            }
            
            App.connection.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            App.connection.Open();
            if (DateDataGrid.SelectedIndex > -1)
            {
                var ci = new DataGridCellInfo(DateDataGrid.Items[DateDataGrid.SelectedIndex], DateDataGrid.Columns[0]);
                var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
                string id_db = content.Text;
                SqlCommand command = new SqlCommand("DELETE FROM \"Table1\" WHERE (Id = " + id_db + ")", App.connection);
                command.ExecuteNonQuery();
                DateDataGrid.ItemsSource = null;
                command = new SqlCommand("SELECT Id, Время, До, Событие, Место FROM Table1 WHERE(Id_ = " + id_db + ")", App.connection);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                DateDataGrid.ItemsSource = dt.DefaultView;
            }
            App.connection.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new AddWindow2(id_db,this).Show();
        }

        private void setColumnsNames(int k)
        {
            DateDataGrid.Columns[1].Header = (k==0) ? "Время" : "Time";
            DateDataGrid.Columns[2].Header = (k == 0) ? "До" : "To";
            DateDataGrid.Columns[3].Header = (k == 0) ? "Событие" : "Event";
            DateDataGrid.Columns[4].Header = (k == 0) ?  "Место" : "Place";
            tableDataGrid.Columns[1].Header = (k == 0) ? "День" : "Day";
            tableDataGrid.Columns[2].Header = (k == 0) ? "Месяц" : "Month";
            tableDataGrid.Columns[3].Header = (k == 0) ?  "Год" : "Year";
        }

        private void RUS_button_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(String.Format("DictionaryRU.xaml"), UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dict);
            setColumnsNames(0);


        }

        private void ENG_button_Click(object sender, RoutedEventArgs e)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(String.Format("DictionaryENG.xaml"), UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dict);
            setColumnsNames(1);

        }
    }
}
