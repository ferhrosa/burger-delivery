using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Burger.Api.Models
{
    public class Ingredient
    {

        [Key]
        public short Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

    }
}