using System.Collections.Generic;

namespace OrganizaCaixas.Dtos.Output
{
    public class CaixaUtilizadaDto
    {
        public string TipoCaixa { get; set; }
        public string DimensoesCaixa { get; set; }
        public List<ProdutoEmbaladoDto> ProdutosEmbalados { get; set; } = new List<ProdutoEmbaladoDto>();
    }
}