﻿using Dapper;
using Microsoft.Extensions.Configuration;
using RecipeFinder.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinder.DataLayer.Repositories
{
    public class RecipeRepository : IRecipeRepository<Recipe>, IRepository<Recipe>
    {
        private readonly string connString;

        public  RecipeRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<Recipe> AddAsync(Recipe entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO Recipe(UserId, Title, Slug, Instruction, CreatedAt) OUTPUT INSERTED.* values (@UserId, @Title, @Slug, @Instruction, @CreatedAt)";

                var result = await db.QueryAsync<Recipe>(sql, new { UserId = entity.UserId, Title = entity.Title, Slug = entity.Slug, Instruction = entity.Instruction, CreatedAt = entity.CreatedAt });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM Recipe WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Recipe>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = $"SELECT * FROM Recipe WHERE [{propName}] = @value";

                var result = await db.QueryAsync<Recipe>(sql, new { value = value });

                return result;
            }
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Recipe ORDER BY CreatedAt DESC";

                return await db.QueryAsync<Recipe>(sql);
            }
        }

        public async Task<Recipe> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM Recipe WHERE Id = @Id";

                var result = await db.QueryAsync<Recipe>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(Recipe entity)
        {
            byte[] rowVersion;
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE Recipe SET Title = @Title, Slug = @Slug, Instruction = @Instruction, CreatedAt = @CreatedAt OUTPUT INSERTED.RowVer WHERE Id = @Id AND RowVer = @RowVer";

                rowVersion = await db.ExecuteScalarAsync<byte[]>(sql, entity);

                if(rowVersion == null)
                {
                    throw new DBConcurrencyException("The entity you were trying to edit has changed. Reload the entity and try again.");
                }

                return rowVersion != null ? 1 : 0;
            }
        }
    }
}
