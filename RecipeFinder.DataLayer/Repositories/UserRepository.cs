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
        public void Create(User entity)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO User(Username, Email, Password, IsAdmin) values (@Username, @Email, @Password, @IsAdmin)";

                db.Execute(sql, new {Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin });
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM User where Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public User Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM User WHERE Id = @Id";

                return db.Query<User>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM User";

                return db.Query<User>(sql).ToList();
            } 
        }

        public IEnumerable<User> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Email and value with e.g. User@email.com
                //Ex: GetAll(nameof(User.Email), "User@email.com")
                string sql = "SELECT * FROM User WHERE @propertyName = @value";

                return db.Query<User>(sql, new { propertyName = propertyName, value = value }).ToList();
            }
        }

        public void Update(User entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE User SET Username = @Username, Email = @Email, Password = @Password, IsAdmin = @IsAdmin";

                db.Execute(sql, new { Username = entity.Username, Email = entity.Email, Password = entity.Password, IsAdmin = entity.IsAdmin });
            }
        }
    }
}
