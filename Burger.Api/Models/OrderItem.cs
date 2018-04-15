using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Burger.Api.Models
{
    public class OrderItem
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required]
        public Order Order { get; set; }

        /// <summary>
        /// The name of the item is replicated from the recipe to the order
        /// because the name on the recipe may change. In case of change, the
        /// orders history aren't affected.
        /// </summary>
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public Sales? Sale { get; set; }

        public string SaleName { get { return Sale?.GetName(); } }

        public List<OrderItemIngredient> Ingredients { get; set; }


        public bool HasIngredient(Ingredients ingredient)
        {
            return Ingredients != null
                && Ingredients.Any(i => i.Ingredient.Id == (short)ingredient);
        }

        public int CountIngredient(Ingredients ingredient)
        {
            if (Ingredients == null) { return 0; }

            return Ingredients
                .Where(i => i.Ingredient.Id == (short)ingredient)
                .Sum(i => i.Ammount);
        }
    }
}