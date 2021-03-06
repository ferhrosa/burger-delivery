﻿using System.Collections.Generic;
using System.Linq;
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


        // GET recipes
        public IEnumerable<Recipe> Get()
        {
            return context.Recipes.Include("Ingredients");
        }

        // GET recipes/5
        public Recipe Get(short id)
        {
            return context.Recipes.Include("Ingredients").FirstOrDefault(r => r.Id == id);
        }

    }
}