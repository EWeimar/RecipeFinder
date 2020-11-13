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
    public class RecipeRepository : IRepository<Recipe>
    {
        private readonly string connString;
        public RecipeRepository(string connString)
        {
            this.connString = connString;
        }

        public void Create(Recipe entity)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Recipe(UserId, Title, Slug, Instruction, CreatedAt) values (@UserId, @Title, @Slug, @Instruction, @CreatedAt)";

                db.Execute(sql, new {UserId = entity.UserId, Title = entity.Title, Slug = entity.Slug, Instruction = entity.Instruction, CreatedAt = entity.CreatedAt });
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Recipe WHERE Id = @Id";
                db.Execute(sql, new { Id = id });
            }
            
        }

        public Recipe Get(int id)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Recipe WHERE Id = @Id";
                return db.Query<Recipe>(sql, new {Id = id }).FirstOrDefault();        
            }
        }

        public IEnumerable<Recipe> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Recipe";
                return db.Query<Recipe>(sql).ToList();
            }
        }

        
        public IEnumerable<Recipe> GetAll(string propertyName, object value)
        {
            using(var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Title and value with e.g. Flæskesteg
                //Ex: GetAll(nameof(Recipe.Title), "Flæskesteg")
                string sql = "SELECT * FROM Recipe WHERE @propertyName = @value";
                return db.Query<Recipe>(sql, new { propertyName = propertyName, value = value }).ToList();
            }
        }

        public void Update(Recipe entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Recipe SET UserId = @UserId, Title = @Title, Slug = @Slug, Instruction = @Instruction, CreatedAt = @CreatedAt WHERE Id = @Id";
                db.Execute(sql, new {UserId = entity.UserId, Title = entity.Title, Slug = entity.Slug, Instruction = entity.Instruction, CreatedAt = entity.CreatedAt, Id = entity.Id });
            }
        }
    }
}
