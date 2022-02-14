using CreateTabels.sqlconnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System.IO;

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
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;

namespace CreateTabels
{
    /// <summary>
    /// Interaction logic for Insart.xaml
    /// </summary>
    public partial class Insart : Window
    {
        List<string> TableColumns = new List<string>();
        string TableName="";
        string colName = "";
        List<string> controlNames = new List<string>();
        string insertimagevalue;
        public Insart(string tablename)
        {
            InitializeComponent();
                Table.Text = tablename;
                TableName = tablename;
                var col= SqlCode.CheckColumns(tablename);
                var colDatatype = SqlCode.CheckColumnsDatatype(tablename);
         


            for (int i = 0; i < col.Count; i++)
                {
                    if (colDatatype[i] == "date")
                    {
                        DatePicker datepicker = new DatePicker();
                        datepicker.Height = 30;
                        datepicker.Width = 280;
                        datepicker.Margin = new Thickness(10);
                        datepicker.Background = new SolidColorBrush(Colors.White);
                        datepicker.Foreground = new SolidColorBrush(Colors.Black);
                        datepicker.Name = "datepicker_" + col[i];
                        MaterialDesignThemes.Wpf.HintAssist.SetHint(datepicker, col[i]);
                        MaterialDesignThemes.Wpf.TextFieldAssist.SetHasClearButton(datepicker, true);
                        controlNames.Add("datepicker_" + col[i]);
                    grid.Items.Add(datepicker);
                        grid.RegisterName(datepicker.Name, datepicker);
                    }
                    else if (colDatatype[i] == "image")
                    {
                        Button button = new Button();
                    button.Height = 30;
                    button.Width = 280;
                    button.Margin = new Thickness(10);
                    button.Background = new SolidColorBrush(Colors.White);
                    button.Foreground = new SolidColorBrush(Colors.Black);
                    button.Name = "AddImage_" + col[i];
                    button.Content = "Add Image";

                    controlNames.Add("AddImage_" + col[i]);
                    button.Click += new RoutedEventHandler(AddImage_Click);

                    grid.Items.Add(button);
                    grid.RegisterName(button.Name, button);

                }
                else
                    {
                        CompoboxDiloag frm2 = new CompoboxDiloag(col[i]);
                        frm2.ShowDialog();

                        if (CompoboxDiloag.x == "Textbox")
                        {
                            TextBox txtb = new TextBox();
                            txtb.Height = 30;
                            txtb.Width = 280;
                            txtb.Margin = new Thickness(10);
                            txtb.Background = new SolidColorBrush(Colors.White);
                            txtb.Foreground = new SolidColorBrush(Colors.Black);
                            txtb.Name = "txt_" + col[i];
                            controlNames.Add("txt_" + col[i]);
                            MaterialDesignThemes.Wpf.HintAssist.SetHint(txtb, col[i]);
                            MaterialDesignThemes.Wpf.TextFieldAssist.SetHasClearButton(txtb, true);
                            grid.Items.Add(txtb);
                            grid.RegisterName(txtb.Name, txtb);
                     
                        frm2.Close();
                        }
                        else if (CompoboxDiloag.x == "ComboBox")
                        {
                            ComboBox combobox = new ComboBox();
                            combobox.Height = 30;
                            combobox.Width = 280;
                            combobox.Margin = new Thickness(10);
                            combobox.Background = new SolidColorBrush(Colors.White);
                            combobox.Foreground = new SolidColorBrush(Colors.Black);
                            combobox.Name = "combobox_" + col[i];
                            controlNames.Add("combobox_" + col[i]);
                            SqlCode.ExecuteComboBox("select " + CompoboxDiloag.Columns + " from " + CompoboxDiloag.Table, combobox);
                            MaterialDesignThemes.Wpf.HintAssist.SetHint(combobox, col[i]);
                            MaterialDesignThemes.Wpf.TextFieldAssist.SetHasClearButton(combobox, true);
                            grid.Items.Add(combobox);
                            grid.RegisterName(combobox.Name, combobox);

                            frm2.Close();
                        }
                    }

                    if (i == col.Count - 1)
                        colName = colName + col[i];
                    else
                        colName = colName + col[i] + " , ";

                }
        }
        private void AddImage_Click(object sender, EventArgs e)
        {
          OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.ShowDialog();

            openFileDialog1.Filter = "Image Files(.BMP; *.JPG; *.GIF)| *.BMP; *.JPG; *.GIF | All files(.) | *.";

            openFileDialog1.DefaultExt = ".jpeg";

            string path = openFileDialog1.FileName;

            insertimagevalue = "(SELECT * FROM OPENROWSET(BULK N'" + path + "', SINGLE_BLOB) as T1)";
       

        }
        private byte[] ImageToStream(string fileName)
        {
            MemoryStream stream = new MemoryStream();
            tryagain:
            try
            {
                Bitmap image = new Bitmap(fileName);
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                goto tryagain;
            }

            return stream.ToArray();
        }


        private void Inserdata_Click(object sender, RoutedEventArgs e)
        {
            
            String values = "";
            foreach (var CN in controlNames)
            {
                TextBox tb = FindName(CN) as TextBox;
                ComboBox cb = FindName(CN) as ComboBox;
                DatePicker dpb = FindName(CN) as DatePicker;
                Button btn = FindName(CN) as Button;

                if (tb == null && cb==null&& btn == null)
                {
                    values = values + "\'" + dpb.Text + "\',";
                }
                else if (dpb == null && cb == null && btn == null)
                {
                    values = values + "\'" + tb.Text + "\',";
                }
                else if (dpb == null && tb == null && btn == null)
                {
                    values = values + "\'" + cb.Text + "\',";
                }
                else if (dpb == null && tb == null && cb == null)
                {
                    values = values + " " + insertimagevalue + " ,";
                }

            }

            values = values.Remove(values.Length - 1);
            MessageBox.Show(values);
            SqlCode.FillCommand(TableName);
            string quary = "insert into " + TableName + " (" + colName + ") values(" + values + ")";
            SqlCode.Execute(quary);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Close();
        }
    }
}
