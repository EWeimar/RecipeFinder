using RecipeFinder.BusinessLayer.Lib;
using RecipeFinder.DataLayer.Models;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands;
using System;

namespace RecipeFinder.DevTools
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Migrate with DbUp");
            Console.WriteLine("2) Seed dummy data");
            Console.WriteLine("3) Hash password");
            Console.WriteLine("q) Exit");
            Console.Write("\r\nSelect an option: ");

            String option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    MigrateDBUPCommand.RunCommand();
                    break;
                case "2":
                    DummyRecipesCommand.RunCommand();
                    break;
                case "3":
                    HashPasswordCommand.RunCommand();
                    break;
                case "q":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }

            Console.WriteLine("\nApp will now close. If you want to run more commands then restart.");
            Console.ReadLine();
        }
    }
}
