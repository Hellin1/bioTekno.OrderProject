using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Entities.Domains
{
    public class Product
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string Unit { get; set; }

        public decimal UnitPrice { get; set; }

        public string Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }


        public List<OrderDetail> OrderDetails { get; set; }
    }
}
