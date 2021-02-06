using System;
using System.IO;
using Microsoft.Office.Interop.Excel;
using SpreadsheetLight;

namespace ConsoleAppExcel
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("What is your first name?");
            string firstName = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            string lastName = Console.ReadLine();

            using (SLDocument sl = new SLDocument())
            {
                sl.SetCellValue("A1", firstName);
                sl.SetCellValue("B1", lastName);

                var text1 = sl.GetCellValueAsString(1, 1);
                Console.WriteLine(text1);

                var text2 = sl.GetCellValueAsString(2, 1);
                Console.WriteLine(text2);

                sl.SaveAs("Example.xlsx");
            }
        }
    }
}
