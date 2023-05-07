using AutoMapper;
using bioTekno.OrderProject.Dtos;
using bioTekno.OrderProject.Entities.Domains;
using bioTekno.OrderProject.UI.Models;

namespace bioTekno.OrderProject.UI.Mappings.AutoMapper
{
    public class CreateOrderRequestProfile : Profile
    {
        public CreateOrderRequestProfile()
        {
            CreateMap<CreateOrderRequestModel, CreateOrderRequest>().ReverseMap();
            
        }
    }
}
