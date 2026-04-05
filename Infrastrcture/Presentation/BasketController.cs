using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManger serviceManger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id)
        {
            var result = await serviceManger.BasketService.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> GetBasketById(BasketDto basketDto)
        {
            var result = await serviceManger.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string id)
        {
          await  serviceManger.BasketService.DeleteBasketAsyc(id);
            return NoContent();
        } 
    }
}
