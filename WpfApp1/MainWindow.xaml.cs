using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using SpreadsheetLight;

namespace WpfApp1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Your name has been saved into .txt, Excel, and the SQL Server!" + '\n' + "First Name: " + this.FirstName.Text + '\n' + "Last Name: " +  this.LastName.Text);


            //Text file portion
            string path = Directory.GetCurrentDirectory();
            using (StreamWriter sw = File.CreateText(path + "//sample.txt"))
            {
                sw.WriteLine(this.FirstName.Text);
                sw.WriteLine(this.LastName.Text);
            }

            try
            {
                using (var sr = new StreamReader("sample.txt"))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (IOException error)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(error.Message);
            }


            //Excel file portion
            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue("A1", FirstName.Text);
                sl.SetCellValue("B1", LastName.Text);

                var text1 = sl.GetCellValueAsString(1, 1);
                Console.WriteLine(text1);

                var text2 = sl.GetCellValueAsString(2, 1);
                Console.WriteLine(text2);

                sl.SaveAs("Example.xlsx");
            }

            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + "\"NABA Database\"";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sqlQuery = "INSERT INTO NamesTable (FirstName, LastName) " + $"VALUES ('{this.FirstName.Text}',  '{this.LastName.Text}') ";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            command.ExecuteNonQuery();

            sqlQuery = $"SELECT TOP 1 * FROM NamesTable WHERE FirstName = '{this.FirstName.Text}' AND LastName = '{this.LastName.Text}'";
            command = new SqlCommand(sqlQuery, conn);

            conn.Close();
        }
    }
}
