using System;

namespace OrganizaCaixas.Dtos.Output
{
    public class ProdutoEmbaladoDto
    {
        public Guid IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DimensoesOriginais { get; set; }
        public string DimensoesNaCaixa { get; set; }
    }
}