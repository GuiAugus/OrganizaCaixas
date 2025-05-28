using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrganizaCaixas.Dtos.Output
{
    public class PedidoResponseOutputDto
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; } 

        [JsonPropertyName("caixas")] 
        public List<CaixaOutputDto> Caixas { get; set; } = new List<CaixaOutputDto>(); // Nome ajustado

    }
}