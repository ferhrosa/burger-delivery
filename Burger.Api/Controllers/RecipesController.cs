using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Burger.Api.Data;
using Burger.Api.Models;

namespace Burger.Api.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly Context context;

        public RecipesController(Context context)
        {
            this.context = context;
        }


        // GET api/recipes
        public IEnumerable<Recipe> Get()
        {
            return context.Recipes.Include("Ingredients");
        }

        // GET api/recipes/5
        public Recipe Get(short id)
        {
            return context.Recipes.Include("Ingredients").FirstOrDefault(r => r.Id == id);
        }

        //// POST api/recipes
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/recipes/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/recipes/5
        //public void Delete(int id)
        //{
        //}
    }
}