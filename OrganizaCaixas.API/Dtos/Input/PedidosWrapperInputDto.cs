using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrganizaCaixas.Dtos.Input
{
    public class PedidosWrapperInputDto
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoRequestInputDto> Pedidos { get; set; } = new List<PedidoRequestInputDto>();
    }
}