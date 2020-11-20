using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace RecipeFinder.DataLayer.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly string connString;

        public UserRepository(string connString)
        {
            this.connString = connString;
        }
        public User Create(User entity)
        {
            using(var db = new SqlConnection(connString))
            {
                if (entity.CreatedAt == DateTime.MinValue)
                {
                    entity.CreatedAt = DateTime.Now;
                }

                string sql = "INSERT INTO Users(Username, Email, Password, IsAdmin, CreatedAt) OUTPUT INSERTED.* values (@Username, @Email, @Password, @IsAdmin, @CreatedAt)";

                return db.Query<User>(sql, new {Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin, CreatedAt = entity.CreatedAt }).Single();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Users where Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public User Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Users WHERE Id = @Id";

                return db.Query<User>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Users";

                return db.Query<User>(sql).ToList();
            } 
        }

        public IEnumerable<User> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Email and value with e.g. User@email.com
                //Ex: GetAll(nameof(User.Email), "User@email.com")
                string sql = $"SELECT * FROM Users WHERE [{propertyName}] = @value";

                return db.Query<User>(sql, new { value = value }).ToList();
            }
        }

        public void Update(User entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Users SET Username = @Username, Email = @Email, Password = @Password, IsAdmin = @IsAdmin WHERE Id = @Id";

                db.Execute(sql, new { Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin, Id = entity.Id });
            }
        }

        public bool ValidLogin(string strUsername, string strPassword)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM Users WHERE Username = @username AND Password = @password";

                //var res = db.Query<User>(sql, new { username = strUsername, password = strPassword }).ToList();
            }

            return true;
        }
    }
}
