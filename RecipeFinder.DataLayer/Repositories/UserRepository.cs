using Dapper;
using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
{
    public class UserRepository : IUserRepository<User>, IRepository<User>
    {
        private readonly string connString;

        public UserRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<User> AddAsync(User entity)
        {
            using (var db = new SqlConnection(connString))
            {

                // if the "CreatedAt" field is leaved empty, assign it with current timestamp
                if (entity.CreatedAt == DateTime.MinValue)
                {
                    entity.CreatedAt = DateTime.Now;
                }

                string sql = "INSERT INTO Users(Username, Email, Password, IsAdmin, CreatedAt) OUTPUT INSERTED.* values (@Username, @Email, @Password, @IsAdmin, @CreatedAt)";
                var result = await db.QueryAsync<User>(sql, new { Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin, CreatedAt = entity.CreatedAt });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Users WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<User>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM Users WHERE [{propName}] = @value";

                return await db.QueryAsync<User>(sql, new { value = value });
            }
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Users";

                return await db.QueryAsync<User>(sql);
            }
        }

        public async Task<User> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Users WHERE Id = @Id";

                var result = await db.QueryAsync<User>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(User entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Users SET Username = @Username, Email = @Email, Password = @Password, IsAdmin = @IsAdmin WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin, Id = entity.Id });
            }
        }

        public async Task<string> GetUserHashedPassword(string username)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM Users WHERE Username = @username";

                var res = await db.QueryAsync<User>(sql, new { username = username });

                if (res.Any())
                {
                    return res.Single().Password.ToString();
                }
            }

            return string.Empty;
        }
    }
}
