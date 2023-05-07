using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
