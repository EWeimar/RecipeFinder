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
    public class ImageRepository : IRepository<Image>
    {
        private readonly string connString;
        public ImageRepository(string connString)
        {
            this.connString = connString;
        }
        public Image Create(Image entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Image(RecipeId, FileName) OUTPUT INSERTED.* values (@RecipeId, @FileName)";

                return db.Query<Image>(sql, new { RecipeId = entity.RecipeId, FileName = entity.FileName }).Single();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Image WHERE Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public Image Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Image WHERE Id = @Id";

                return db.Query<Image>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Image> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Image";

                return db.Query<Image>(sql).ToList();
            }
        }

        public IEnumerable<Image> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. FileName and value with e.g. Flæskesteg.jpg
                //Ex: GetAll(nameof(Image.FileName), "Flæskesteg.jpg"
                string sql = $"SELECT * FROM Image WHERE [{propertyName}] = @value";

                return db.Query<Image>(sql, new { value = value }).ToList();
            }
        }

        public void Update(Image entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Image SET RecipeId = @RecipeId, FileName = @FileName";

                db.Execute(sql, new { RecipeId = entity.RecipeId, FileName = entity.FileName });
            }
        }
    }
}
