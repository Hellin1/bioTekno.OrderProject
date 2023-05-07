using bioTekno.OrderProject.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.UI.Models
{
    public class CreateOrderRequestModel
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGSM { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }
}
