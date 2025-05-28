using System;

namespace OrganizaCaixas.Dtos.Output
{
    public class ProdutoEmbaladoDto
    {
        public Guid IdProduto { get; set; }
        public string NomeProduto { get; set; } = string.Empty; 
        public string DimensoesOriginais { get; set; } = string.Empty;
        public string DimensoesNaCaixa { get; set; } = string.Empty;
    }
}