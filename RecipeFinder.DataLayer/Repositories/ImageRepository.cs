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
    public class ImageRepository : IRepository<Image>
    {
        private readonly string connString;

        public ImageRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<Image> AddAsync(Image entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Image(RecipeId, FileName) OUTPUT INSERTED.* values (@RecipeId, @FileName)";

                var result = await db.QueryAsync<Image>(sql, new { RecipeId = entity.RecipeId, FileName = entity.FileName });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Image WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Image>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. FileName and value with e.g. Flæskesteg.jpg
                //Ex: GetAll(nameof(Image.FileName), "Flæskesteg.jpg"
                string sql = $"SELECT * FROM Image WHERE [{propName}] = @value";

                return await db.QueryAsync<Image>(sql, new { value = value });
            }
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Image";

                return await db.QueryAsync<Image>(sql);
            }
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Image WHERE Id = @Id";

                var result = await db.QueryAsync<Image>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(Image entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Image SET FileName = @FileName WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { FileName = entity.FileName, Id = entity.Id });
            }
        }
    }
}
