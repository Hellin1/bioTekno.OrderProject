using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Entities.Domains
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string CustomerGSM { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
