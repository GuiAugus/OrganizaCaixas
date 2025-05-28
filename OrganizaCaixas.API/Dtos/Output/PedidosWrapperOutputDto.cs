using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrganizaCaixas.Dtos.Output
{
    public class PedidosWrapperOutputDto
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoResponseOutputDto> Pedidos { get; set; } = new List<PedidoResponseOutputDto>();
    }
}