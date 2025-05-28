using Microsoft.AspNetCore.Mvc;
using OrganizaCaixas.Dtos.Input;
using OrganizaCaixas.Dtos.Output;
using OrganizaCaixas.Services; 
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OrganizaCaixas.API.Controllers
{
    [ApiController] 
    [Route("api/[controller]")] 
    public class PackagingController : ControllerBase 
    {
        private readonly IPackagingService _packagingService;

        public PackagingController(IPackagingService packagingService)
        {
            _packagingService = packagingService;
        }

        [HttpPost("pack-orders")] 
        [ProducesResponseType(typeof(PedidosWrapperOutputDto), 200)] 
        [ProducesResponseType(typeof(string), 400)] 
        public async Task<IActionResult> PackOrders([FromBody] PedidosWrapperInputDto pedidosInput)
        {
            if (pedidosInput == null || !pedidosInput.Pedidos.Any())
            {
                return BadRequest("A requisição deve conter uma lista de pedidos válida.");
            }

            var resultados = await _packagingService.ProcessarPedidosAsync(pedidosInput);

            return Ok(resultados);
        }
    }
}