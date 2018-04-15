using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Burger.Api.Models
{
    public class OrderItemIngredient
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }

        [Required]
        public OrderItem OrderItem { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }

        /// <summary>
        /// The price is replicated from the ingredient because it may change.
        /// In case of change, the orders history aren't affected.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        [Required]
        public byte Ammount { get; set; }

    }
}