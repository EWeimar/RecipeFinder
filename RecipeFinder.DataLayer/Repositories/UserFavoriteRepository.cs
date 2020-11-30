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
    public class UserFavoriteRepository : IRepository<UserFavorite>
    {
        private readonly string connString;

        public UserFavoriteRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<UserFavorite> AddAsync(UserFavorite entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO UserFavorite(UserId, RecipeId) OUTPUT INSERTED.* values (@UserId, @RecipeId)";

                var result = await db.QueryAsync<UserFavorite>(sql, new { UserId = entity.UserId, RecipeId = entity.RecipeId });
                
                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM UserFavorite WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<UserFavorite>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. UserId and value with e.g. 2
                //Ex: GetAll(nameof(UserFavorite.UserId), 2)
                string sql = $"SELECT * FROM UserFavorite WHERE [{propName}] = @value";

                return await db.QueryAsync<UserFavorite>(sql, new { value = value });
            }
        }

        public async Task<IEnumerable<UserFavorite>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM UserFavorite";

                return await db.QueryAsync<UserFavorite>(sql);
            }
        }

        public async Task<UserFavorite> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM UserFavorite WHERE Id = @Id";

                var result = await db.QueryAsync<UserFavorite>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(UserFavorite entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE UserFavorite SET UserId = @UserId, RecipeId = @RecipeId WHERE Id = @Id";

                var result = await db.ExecuteAsync(sql, new { UserId = entity.UserId, RecipeId = entity.RecipeId, Id = entity.Id });

                return result;
            }
        }
    }
}
