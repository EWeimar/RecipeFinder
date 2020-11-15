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
        public void Create(UserFavorite entity)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO UserFavorite(UserId, RecipeId) values (@UserId, @RecipeId)";

                db.Execute(sql, new { UserId = entity.UserId, RecipeId = entity.RecipeId });

            }
        }

        public void Delete(int id)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM UserFavorite WHERE Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public UserFavorite Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM UserFavorite WHERE Id = @Id";

                return db.Query<UserFavorite>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<UserFavorite> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM UserFavorite";

                return db.Query<UserFavorite>(sql).ToList();
            }
        }

        public IEnumerable<UserFavorite> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. UserId and value with e.g. 2
                //Ex: GetAll(nameof(UserFavorite.UserId), 2)
                string sql = "SELECT * FROM UserFavorite WHERE @propertyName = @value";

                return db.Query<UserFavorite>(sql, new { propertyName = propertyName, value = value }).ToList();
            }
        }

        public void Update(UserFavorite entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE UserFavorite SET UserId = @UserId, RecipeId = @RecipeId";

                db.Execute(sql, new { UserId = entity.UserId, RecipeId = entity.RecipeId });
            }
        }
    }
}
