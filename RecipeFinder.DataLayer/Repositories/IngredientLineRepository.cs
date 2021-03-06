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
    public class IngredientLineRepository : IRepository<IngredientLine>
    {
        private readonly string connString;

        public IngredientLineRepository(string connString)
        {
            this.connString = connString;
        }

        public async Task<IngredientLine> AddAsync(IngredientLine entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO IngredientLine(RecipeId, IngredientId, Amount, MeasureUnit) OUTPUT INSERTED.* values (@RecipeId, @IngredientId, @Amount, @MeasureUnit)";

                var result = await db.QueryAsync<IngredientLine>(sql, new { RecipeId = entity.RecipeId, IngredientId = entity.IngredientId, Amount = entity.Amount, MeasureUnit = entity.MeasureUnit });

                return result.Single();
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM IngredientLine WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<IngredientLine>> FindByCondition(string propName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Amount and value with e.g. 4
                //Ex: GetAll(nameof(IngredientLine.Amount), 4)
                string sql = $"SELECT * FROM IngredientLine WHERE [{propName}] = @value";

                return await db.QueryAsync<IngredientLine>(sql, new { value = value });
            }
        }

        public async Task<IEnumerable<IngredientLine>> GetAllAsync()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM IngredientLine";

                return await db.QueryAsync<IngredientLine>(sql);
            }
        }

        public async Task<IngredientLine> GetByIdAsync(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM IngredientLine WHERE Id = @Id";

                var result = await db.QueryAsync<IngredientLine>(sql, new { Id = id });

                return result.Single();
            }
        }

        public async Task<int> UpdateAsync(IngredientLine entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE IngredientLine SET IngredientId = @IngredientId, Amount = @Amount, MeasureUnit = @MeasureUnit WHERE Id = @Id";

                return await db.ExecuteAsync(sql, new { IngredientId = entity.IngredientId, Amount = entity.Amount, MeasureUnit = entity.MeasureUnit, Id = entity.Id });
            }
        }
    }
}
