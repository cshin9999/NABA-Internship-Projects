using System;
using System.Data.SqlClient;
using System.Text;

namespace SQLApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your first name?");
            string firstName = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            string lastName = Console.ReadLine();
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + "\"NABA Database\"";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string sqlQuery = "INSERT INTO NamesTable (FirstName, LastName) " + $"VALUES ('{firstName}',  '{lastName}') ";
            SqlCommand command = new SqlCommand(sqlQuery, conn);
            command.ExecuteNonQuery();

            sqlQuery = $"SELECT TOP 1 * FROM NamesTable WHERE FirstName = '{firstName}' AND LastName = '{lastName}'";
            command = new SqlCommand(sqlQuery, conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    Console.WriteLine();
                    Console.WriteLine(reader["FirstName"].ToString());
                    Console.WriteLine(reader["LastName"].ToString());
                }
            }

            conn.Close();
            Console.WriteLine("done");
        }
    }
}
