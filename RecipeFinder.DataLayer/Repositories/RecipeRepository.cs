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
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Recipe Get(int id)
        {
            using(var db = new SqlConnection(connString))
            {           
                return db.Query<Recipe>("SELECT * FROM Recipe WHERE ID = @recipeId", new {recipeId = id }).FirstOrDefault();        
            }
        }

        public IEnumerable<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Recipe entity)
        {
            throw new NotImplementedException();
        }
    }
}
