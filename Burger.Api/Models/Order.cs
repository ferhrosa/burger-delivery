using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Burger.Api.Models
{
    public class Order
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime EntryDate { get; set; }

        public string FormattedEntryDate { get { return EntryDate.ToString("dd/MM/yyyy HH:mm:ss"); } }

        public List<OrderItem> Items { get; set; }

        public decimal? Price { get { return Items?.Sum(i => i.Price); } }


        public Order()
        {
            // The default value for the EntryDate is "now". So, when creating a
            // new order, the entry date will be correctly filed.
            EntryDate = DateTime.Now;
        }

    }
}