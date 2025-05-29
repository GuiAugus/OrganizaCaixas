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
        
        [HttpGet("pedidos")]
        [ProducesResponseType(typeof(List<PedidoResponseOutputDto>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetOrderHistory()
        {
            try
            {
                var history = await _packagingService.GetOrderHistoryAsync();
                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar histórico: {ex.Message}");
            }
        }

        [HttpGet("pedidos/{pedidoId}")] 
        [ProducesResponseType(typeof(PedidoResponseOutputDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> GetOrderHistoryById(int pedidoId)
        {
            try
            {
                var pedido = await _packagingService.GetOrderHistoryByIdAsync(pedidoId);
                if (pedido == null)
                {
                    return NotFound($"Pedido com ID {pedidoId} não encontrado no histórico.");
                }
                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao buscar pedido {pedidoId}: {ex.Message}");
            }
        }
    }
}