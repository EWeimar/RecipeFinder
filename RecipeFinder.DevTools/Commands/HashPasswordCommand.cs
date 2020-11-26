using System;
using RecipeFinder.DataLayer.Models;
using RestSharp;
using RecipeFinder.DataLayer.Repositories;
using RecipeFinder.DevTools.Commands.MealDB;
using RecipeFinder.DTO;
using System.Collections.Generic;
using RecipeFinder.BusinessLayer.Services;
using System.Linq;
using System.Threading;
using RecipeFinder.BusinessLayer.Lib;

namespace RecipeFinder.DevTools.Commands
{
    public class HashPasswordCommand
    {
        public static void RunCommand()
        {
            string clearTextPassword = null;

            while (string.IsNullOrEmpty(clearTextPassword))
            {
                Console.WriteLine("Please type a clear text password phrase:");
                clearTextPassword = Console.ReadLine();
            }

            if (!string.IsNullOrEmpty(clearTextPassword))
            {
                Console.WriteLine(SecurePasswordHasher.Hash(clearTextPassword));
            }

            Console.ReadLine();
        }
    }
}
