using OrderManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Services.Model
{
    public class OrderInfo
    {
        public int OrderId { get; set; }

        public Customer Customer { get; set; }

        public List<ProductInfo> ProductList { get; set; }
    }
}
