using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OrganizaCaixas.Dtos.Output
{
    public class CaixaOutputDto
    {
        [JsonPropertyName("caixa_id")]
        public string? CaixaId { get; set; }

        [JsonPropertyName("produtos")]
        public List<string> Produtos { get; set; } = new List<string>();

        [JsonPropertyName("observacao")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Observacao { get; set; }
    }    
}