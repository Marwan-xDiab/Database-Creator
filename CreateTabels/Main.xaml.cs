using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using CreateTabels.sqlconnection;

namespace CreateTabels
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();

            Properties.Settings.Default.DatabaseName = "Database";
            Properties.Settings.Default.Save();
            string query = "SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + Properties.Settings.Default.DatabaseName + "' ";
            sqlconnection.SqlCode.ExecuteDataGrid(query,grid);
  
        }
  
        private void CreateInsert_Click(object sender, RoutedEventArgs e)
        {
            TextBlock x;
        

            foreach (var item in this.grid.SelectedItems)
            {
                x = grid.Columns[0].GetCellContent(item) as TextBlock;

                Insart insart = new Insart(x.Text);
                insart.Show();
                this.Hide();
            }
        }
    }
}
