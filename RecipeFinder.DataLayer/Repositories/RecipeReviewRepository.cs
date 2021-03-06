﻿using Dapper;
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

        public async Task<RecipeReview> AddAsync(RecipeReview entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO RecipeReview(RecipeId, Reviewer, Rating, Comment, CreatedAt) OUTPUT INSERTED.* values (@RecipeId, @Reviewer, @Rating, @Comment, @CreatedAt)";

                var result = await db.QueryAsync<RecipeReview>(sql, new { RecipeId = entity.RecipeId, Reviewer = entity.Reviewer, Rating = entity.Rating, Comment = entity.Comment, CreatedAt = entity.CreatedAt });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM RecipeReview WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<RecipeReview>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM RecipeReview WHERE [{propName}] = @value";

                return await db.QueryAsync<RecipeReview>(sql, new { value = value });
            }
        }

        public async Task<IEnumerable<RecipeReview>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM RecipeReview";

                return await db.QueryAsync<RecipeReview>(sql);
            }
        }

        public async Task<RecipeReview> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM RecipeReview WHERE Id = @Id";

                var result = await db.QueryAsync<RecipeReview>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(RecipeReview entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE RecipeReview SET Reviewer = @Reviewer, Rating = @Rating, Comment = @Comment, CreatedAt = @CreatedAt WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Reviewer = entity.Reviewer, Rating = entity.Rating, Comment = entity.Comment, CreatedAt = entity.CreatedAt, Id = entity.Id });
            }
        }
    }
}
