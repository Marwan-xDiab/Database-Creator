using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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


namespace CreateTabels
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            String str;
            SqlConnection myConn = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE " + DatabaseName.Text + " ON PRIMARY " +
                "(NAME = " + DatabaseName.Text + "_Data, " +
                "FILENAME = 'D:\\" + DatabaseName.Text + ".mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = " + DatabaseName.Text + "_Log, " +
                "FILENAME = 'D:\\" + DatabaseName.Text + "Log.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";
                
            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                Properties.Settings.Default.DatabaseName = DatabaseName.Text;
                Properties.Settings.Default.Save();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram");
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }


            Tabels t = new Tabels();
            t.Show();
            this.Hide();
        }
    }
}
