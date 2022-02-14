using CreateTabels.sqlconnection;
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

namespace CreateTabels
{
    /// <summary>
    /// Interaction logic for CompoboxDiloag.xaml
    /// </summary>  
    public partial class CompoboxDiloag : Window
    {
        public static string x;
        public static string Table;
        public static string Columns;


        public CompoboxDiloag(string colq)
        {
            InitializeComponent();
            col.Text = colq;
            string query = "SELECT TABLE_NAME  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' AND TABLE_CATALOG = '" + Properties.Settings.Default.DatabaseName + "' ";
            SqlCode.ExecuteComboBox(query, Tablss);
        }

        private void clicks_Click(object sender, RoutedEventArgs e)
        {
            x = com.Text;

            if (x == "ComboBox") {
                Table = Tablss.SelectedValue.ToString();
                Columns = Culs.SelectedValue.ToString();
            }

            this.Close();
        }

        private void Tablss_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var col = SqlCode.CheckColumns(Tablss.SelectedValue.ToString());
            Culs.ItemsSource = col;

        }

        private void Culs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void com_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            

        } 
    
            
     
    }
}
