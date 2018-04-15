using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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

        public List<OrderItemIngredient> Ingredients { get; set; }

    }
}