using System.Text.Json.Serialization; 

namespace OrganizaCaixas.Dtos.Input
{
    public class ProdutoInputDto
    {
        [JsonPropertyName("produto_id")] 
        public string ?ProdutoId { get; set; } 

        [JsonPropertyName("dimensoes")] 
        public DimensaoCaixasInputDto ?Dimensoes { get; set; } 
    }
}