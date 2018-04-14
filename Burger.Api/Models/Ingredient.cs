using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Burger.Api.Models
{
    public class Ingredient
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public short Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public ICollection<Recipe> Recipes { get; set; }
        
    }
}