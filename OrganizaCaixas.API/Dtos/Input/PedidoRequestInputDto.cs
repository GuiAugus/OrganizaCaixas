using System.Collections.Generic;
using System.Text.Json.Serialization; 
namespace OrganizaCaixas.Dtos.Input
{
    public class PedidoRequestInputDto
    {
        [JsonPropertyName("pedido_id")] 
        public int PedidoId { get; set; } 

        [JsonPropertyName("produtos")] 
        public List<ProdutoInputDto> Produtos { get; set; } = new List<ProdutoInputDto>(); // Lista de produtos dentro do pedido
    }
}