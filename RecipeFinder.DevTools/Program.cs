﻿using RecipeFinder.DevTools.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DevTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Seed dummy recipes");
            Console.WriteLine("q) Exit");
            Console.Write("\r\nSelect an option: ");

            String option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    DummyRecipes.runCommand();
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
