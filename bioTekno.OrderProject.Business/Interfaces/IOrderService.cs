using bioTekno.OrderProject.Common.CommonObjects;
using bioTekno.OrderProject.Dtos;
using bioTekno.OrderProject.Entities.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Interfaces
{
    public interface IOrderService
    {
        Task<IApiResponse<List<ProductDto>>> GetProducts(string categoryUrl);

        Task<IApiResponse<int>> Create(CreateOrderRequest request);

        Task<IApiResponse<List<ProductDto>>> GetAllCategory(string categoryUrl);


    }
}
