using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.Tests.Repositories
{
    public class DBHelper
    {
        public static void CreateTestData(string connectionString)
        {
            CreateTestUser(connectionString);
            CreateTestRecipe(connectionString);
            CreateTestIngredient(connectionString);
        }
        public static void CreateTestUser(string connectionString)
        {
            string testUserQuery = "INSERT INTO Users(Username, Email, Password, IsAdmin, CreatedAt) values(@Username, @Email, @Password, @IsAdmin, @CreatedAt)";
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                using (SqlCommand cmd = new SqlCommand(testUserQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@Username", "TestUser");
                    cmd.Parameters.AddWithValue("@Email", "TestEmail");
                    cmd.Parameters.AddWithValue("@Password", "TestPassword");
                    cmd.Parameters.AddWithValue("@IsAdmin", false);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTestRecipe(string connectionString)
        {
            string testRecipeQuery = "INSERT INTO Recipe(UserId, Title, Slug, Instruction, CreatedAt) values (@UserId, @Title, @Slug, @Instruction, @CreatedAt)";
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                using (SqlCommand cmd = new SqlCommand(testRecipeQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@UserId", 1);
                    cmd.Parameters.AddWithValue("@Title", "TestTitle");
                    cmd.Parameters.AddWithValue("@Slug", "TestSlug");
                    cmd.Parameters.AddWithValue("@Instruction", "TestInstruction");
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CreateTestIngredient(string connectionString)
        {
            string testIngredientQuery = "INSERT INTO Ingredient(Name) values (@Name)";
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                using (SqlCommand cmd = new SqlCommand(testIngredientQuery, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@Name", "TestIngredient");

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void CleanDatabase(string connectionString)
        {
            string ingredientQuery = "DELETE FROM Ingredient";
            string ingredientLineQuery = "DELETE FROM IngredientLine";
            string imageQuery = "DELETE FROM Image";
            string recipeQuery = "DELETE FROM Recipe";
            string userQuery = "DELETE FROM Users";

            /* Taken from https://stackoverflow.com/questions/253849/cannot-truncate-table-because-it-is-being-referenced-by-a-foreign-key-constraint */
            string reseedUserIds = "DBCC CHECKIDENT ([Users], RESEED, 0)";
            string reseedRecipeIds = "DBCC CHECKIDENT ([Recipe], RESEED, 0)";
            string reseedIngredientIds = "DBCC CHECKIDENT ([Ingredient], RESEED, 0)";

            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(imageQuery, dbConnection))
                {
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = ingredientLineQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = ingredientQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = recipeQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = userQuery;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = reseedUserIds;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = reseedRecipeIds;
                    sqlCommand.ExecuteNonQuery();

                    sqlCommand.CommandText = reseedIngredientIds;
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}

