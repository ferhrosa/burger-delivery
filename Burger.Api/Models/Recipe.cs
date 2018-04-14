using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Burger.Api.Models
{
    public class Recipe
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public short Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }

        /// <summary>
        /// The price of the recipe is determined by the sum of the prices of its ingredients.
        /// </summary>
        public decimal? Price { get { return Ingredients?.Sum(i => i.Price); } }

    }
}