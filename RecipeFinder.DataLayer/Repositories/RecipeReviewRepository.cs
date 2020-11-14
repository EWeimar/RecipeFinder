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
    public class RecipeReviewRepository : IRepository<RecipeReview>
    {
        private readonly string connString;
        public RecipeReviewRepository(string connString)
        {
            this.connString = connString;
        }
        public void Create(RecipeReview entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO RecipeReview(RecipeId, Reviewer, Rating, Comment, CreatedAt) values (@RecipeId, @Reviewer, @Rating, @Comment, @CreatedAt)";

                db.Execute(sql, new { RecipeId = entity.RecipeId, Reviewer = entity.Reviewer, Rating = entity.Rating, Comment = entity.Comment, CreatedAt = entity.CreatedAt });
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM RecipeReview WHERE Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public RecipeReview Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM RecipeReview WHERE Id = @Id";

                return db.Query<RecipeReview>(sql, new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<RecipeReview> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM RecipeReview";

                return db.Query<RecipeReview>(sql).ToList();
            }
        }

        public IEnumerable<RecipeReview> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Rating and values with e.g. 4
                //Ex: GetAll(nameof(RecipeReview.Rating), 4) 
                string sql = "SELECT * FROM RecipeReview WHERE @propertyName = @value";

                return db.Query<RecipeReview>(sql, new { propertyName = propertyName, value = value }).ToList();
            }
        }

        public void Update(RecipeReview entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE RecipeReview SET RecipeId = @RecipeId, Reviewer = @Reviewer, Rating = @Rating, Comment = @Comment, CreatedAt = @CreatedAt";

                db.Execute(sql, new { RecipeId = entity.RecipeId, Reviewer = entity.Reviewer, Rating = entity.Rating, Comment = entity.Comment, CreatedAt = entity.CreatedAt });
            }
        }
    }
}
