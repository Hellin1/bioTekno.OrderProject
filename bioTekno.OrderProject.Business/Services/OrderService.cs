using AutoMapper;
using bioTekno.OrderProject.Business.Helpers;
using bioTekno.OrderProject.Business.Interfaces;
using bioTekno.OrderProject.Common.CommonObjects;
using bioTekno.OrderProject.DataAccess.Uow;
using bioTekno.OrderProject.Dtos;
using bioTekno.OrderProject.Entities.Domains;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Services
{
    public class OrderService : IOrderService
    {

        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public ICacheService CacheService { get; }


        public OrderService(IUow uow, IMapper mapper, ICacheService cacheService)
        {
            _uow = uow;
            _mapper = mapper;
            CacheService = cacheService;
        }



        // create order
        public async Task<IApiResponse<int>> Create(CreateOrderRequest request)
        {
            
            var details = _mapper.Map<List<OrderDetail>>(request.ProductDetails);
            decimal totalAmount = 0;
            foreach (var productDetail in request.ProductDetails)
            {
                
                totalAmount += productDetail.Amount * productDetail.UnitPrice;
            }

            var order = new Order
            {
                CustomerEmail = request.CustomerEmail,
                CustomerGSM = request.CustomerGSM,
                CustomerName = request.CustomerName,
                OrderDetails = details,
                TotalAmount = totalAmount
            };


            await _uow.GetRepository<Order>().CreateAsync(order);

            await _uow.SaveChangesAsync();

            var publisher = new RabbitMQService("localhost", "myqueue");
            publisher.Publish($"New form submission from nowhere");
            publisher.Dispose();



            return new ApiResponse<int>(Status.Success, order.Id);           
        }

        public async Task<IApiResponse<List<ProductDto>>> GetProducts(string categoryUrl)
        {
            
            List<ProductDto> products = new();
            if (string.IsNullOrEmpty(categoryUrl))
            {

                var data = _uow.GetRepository<Product>().GetQuery().Where(x => x.Category == categoryUrl).ToList();
                products = _mapper.Map<List<ProductDto>>(data);
            }
            else
            {
                var data = await _uow.GetRepository<Product>().GetAllAsync();
                products = _mapper.Map<List<ProductDto>>(data);
            }

            return new ApiResponse<List<ProductDto>>(Status.Success, products);
        }


        public async Task<IApiResponse<List<ProductDto>>> GetAllCategory(string categoryUrl)
        {
            return await GetCategoriesFromCache(categoryUrl);

        }
        private async Task<IApiResponse<List<ProductDto>>> GetCategoriesFromCache(string categoryUrl)
        {
            List<ProductDto> products = new();

            var result = await CacheService.GetOrCreateAsync<List<ProductDto>>(categoryUrl, async () =>
            {
                if (!string.IsNullOrEmpty(categoryUrl))
                {

                    var data = _uow.GetRepository<Product>().GetQuery().Where(x => x.Category == categoryUrl).ToList();
                    products = _mapper.Map<List<ProductDto>>(data);
                }
                else
                {
                    var data = await _uow.GetRepository<Product>().GetAllAsync();
                    products = _mapper.Map<List<ProductDto>>(data);
                }
                return products;
            });


            return new ApiResponse<List<ProductDto>>(Status.Success, result); ;


            
        }
    }
}
