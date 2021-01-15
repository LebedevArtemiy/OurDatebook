using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Datebook
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["Datebook.Properties.Settings.DatabaseConnectionString"].ConnectionString;
        public static SqlConnection connection = new SqlConnection(connectionString);
    }
}
