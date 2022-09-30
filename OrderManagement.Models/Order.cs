using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace OrderManagement.Models
{
    public class Order
    {
        public Order() 
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        
        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        public int CustomerId { get; set; } 

        public virtual ICollection<OrderItem> OrderItems { get; set;}
    }
}
