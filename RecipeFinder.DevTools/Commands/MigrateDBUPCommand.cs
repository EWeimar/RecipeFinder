using DbUp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DevTools.Commands
{
    public class MigrateDBUPCommand
    {
        public static void RunCommand() {
            var connectionString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinderDB;Integrated Security=True;";
            var testConnectionString = @"Data Source=.\SQLExpress;Initial Catalog=RecipeFinderDB_TEST;Integrated Security=True;";

            EnsureDatabase.For.SqlDatabase(connectionString);
            EnsureDatabase.For.SqlDatabase(testConnectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    //.WithScriptsFromFileSystem(@"DatabaseScripts/")
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            var testUpgrader =
              DeployChanges.To
                  .SqlDatabase(testConnectionString)
                  .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                  //.WithScriptsFromFileSystem(@"DatabaseScripts/")
                  .LogToConsole()
                  .Build();

            var testResult = testUpgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();

            Console.ReadLine();
        }
    }
}