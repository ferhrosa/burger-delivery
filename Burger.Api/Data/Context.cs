using System.Data.Entity;
using Burger.Api.Models;

namespace Burger.Api.Data
{
    public class Context : DbContext
    {

        public Context() : base("Burger")
        {
            Database.SetInitializer(new ContextInitializer());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemIngredient> OrderItemIngredients { get; set; }

    }
}