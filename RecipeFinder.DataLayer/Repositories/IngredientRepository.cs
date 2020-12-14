using Dapper;
using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
{
    public class IngredientRepository : IRepository<Ingredient>
    {
        private readonly string connString;

        public IngredientRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<Ingredient> AddAsync(Ingredient entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Ingredient(Name) OUTPUT INSERTED.* values (@Name)";

                var result = await db.QueryAsync<Ingredient>(sql, new { Name = entity.Name });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Ingredient WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Ingredient>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM Ingredient WHERE [{propName}] = @value";

                return await db.QueryAsync<Ingredient>(sql, new { value = value });
            }
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Ingredient";

                return await db.QueryAsync<Ingredient>(sql);
            }
        }

        public async Task<Ingredient> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Ingredient WHERE Id = @Id";

                var result = await db.QueryAsync<Ingredient>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(Ingredient entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Ingredient SET Name = @Name WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Name = entity.Name });
            }
        }
    }
}
