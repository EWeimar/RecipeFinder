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
        public IngredientLine Create(IngredientLine entity)
        {
            using(var db = new SqlConnection(connString))
            {
                string sql = "INSERT INTO IngredientLine(RecipeId, IngredientId, Amount, MeasureUnit) OUTPUT INSERTED.* values (@RecipeId, @IngredientId, @Amount, @MeasureUnit)";

                return db.Query<IngredientLine>(sql, new { RecipeId = entity.RecipeId, IngredientId = entity.IngredientId, Amount = entity.Amount, MeasureUnit = entity.MeasureUnit }).Single();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "DELETE FROM IngredientLine WHERE Id = @Id";

                db.Execute(sql, new { Id = id });
            }
        }

        public IngredientLine Get(int id)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM IngredientLine WHERE Id = @Id";

                return db.Query<IngredientLine>(sql, new { Id =  id }).FirstOrDefault();
            }
        }

        public IEnumerable<IngredientLine> GetAll()
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "SELECT * FROM IngredientLine";

                return db.Query<IngredientLine>(sql).ToList();
            }
        }

        public IEnumerable<IngredientLine> GetAll(string propertyName, object value)
        {
            using (var db = new SqlConnection(connString))
            {
                //Replace propertyName with e.g. Amount and value with e.g. 4
                //Ex: GetAll(nameof(IngredientLine.Amount), 4)
                string sql = $"SELECT * FROM IngredientLine WHERE [{propertyName}] = @value";

                return db.Query<IngredientLine>(sql, new { value = value }).ToList();
            }
        }

        public void Update(IngredientLine entity)
        {
            using (var db = new SqlConnection(connString))
            {
                string sql = "UPDATE IngredientLine SET IngredientId = @IngredientId, Amount = @Amount, MeasureUnit = @MeasureUnit WHERE Id = @Id";

                db.Execute(sql, new { IngredientId = entity.IngredientId, Amount = entity.Amount, MeasureUnit = entity.MeasureUnit, Id = entity.Id });
            }
        }
    }
}
