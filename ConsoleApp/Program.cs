using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your first name?");
            string firstName = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            string lastName = Console.ReadLine();

            string path = Directory.GetCurrentDirectory();
            using (StreamWriter sw = File.CreateText(path + "//sample.txt"))
            {
                sw.WriteLine(firstName);
                sw.WriteLine(lastName);
            }

            try
            {
                using (var sr = new StreamReader("sample.txt"))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
