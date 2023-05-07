using AutoMapper;
using bioTekno.OrderProject.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Mappings.AutoMapper
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, ProductDetail>().ReverseMap();
        }
    }
}
