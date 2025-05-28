using System;
using System.Collections.Generic;

namespace OrganizaCaixas.Dtos.Output
{
    public class PedidoOutputDto
    {
        public Guid IdPedido { get; set; }
        public List<CaixaUtilizadaDto> CaixasUtilizadas { get; set; } = new List<CaixaUtilizadaDto>();
        public int TotalCaixas1 { get; set; }
        public int TotalCaixas2 { get; set; }
        public int TotalCaixas3 { get; set; }
    }
}