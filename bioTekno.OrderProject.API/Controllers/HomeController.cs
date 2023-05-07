using AutoMapper;
using bioTekno.OrderProject.Business.Interfaces;
using bioTekno.OrderProject.Dtos;
using bioTekno.OrderProject.Entities.Domains;
using bioTekno.OrderProject.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bioTekno.OrderProject.UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;


        public HomeController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(string? category)
        {
            
            // apiResponse Data alanında =>  List<ProductDto> olarak dönülecek
            

            //var result = await _orderService.GetProducts(category);
            var result = await _orderService.GetAllCategory(category);




            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderRequestModel request)
        {
            var dto = _mapper.Map<CreateOrderRequest>(request);
            var result = await _orderService.Create(dto);

            

            return Created("", result.Data);
        }


        [HttpGet("{id}")]
        public IActionResult deneme(string id)
        {

            return Ok();
        }
    }
}
