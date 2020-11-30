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

        public Ingredient Create(Ingredient entity)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Ingredient(Name) OUTPUT INSERTED.* values (@Name)";

                return db.Query<Ingredient>(sql, new { Name = entity.Name }).Single();

            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Ingredient WHERE Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public Ingredient Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Ingredient WHERE Id = @Id";

                return db.Query<Ingredient>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Ingredient> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Ingredient";

                return db.Query<Ingredient>(sql).ToList();
            }
        }

        public IEnumerable<Ingredient> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Name and value with e.g. Sukker
                //Ex: GetAll(nameof(Ingredient.Name), "Sukker")
                string sql = $"SELECT * FROM Ingredient WHERE [{propertyName}] = @value";

                return db.Query<Ingredient>(sql, new { value = value }).ToList();
            }
        }

        public void Update(Ingredient entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Ingredient SET Name = @Name WHERE Id = @Id";

                db.Execute(sql, new { Name = entity.Name });
            }
        }
    }
}
