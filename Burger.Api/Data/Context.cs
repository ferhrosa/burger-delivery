using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Burger.Api.Models;

namespace Burger.Api.Data
{
    public class Context : DbContext
    {

        public Context() : base("Burger")
        {
            Database.SetInitializer(new ContextInitializer());
            //Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

    }
}