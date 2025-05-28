// Models/CaixaAtual.cs
using System.Collections.Generic;
using System.Linq;
using System;

namespace OrganizaCaixas.Models
{
    public class CaixaAtual
    {
        public Caixa CaixaBase { get; }
        public List<Produto> ProdutosEmbalados { get; private set; }

        public CaixaAtual(Caixa caixaBase)
        {
            CaixaBase = caixaBase;
            ProdutosEmbalados = new List<Produto>();
        }

        public bool TentarAdicionarProduto(Produto produtoParaAdicionar, out (decimal h, decimal l, decimal c) dimensoesUsadas)
        {
            dimensoesUsadas = (0, 0, 0);

            decimal volumeOcupadoAtual = ProdutosEmbalados.Sum(p => p.Volume);
            if ((volumeOcupadoAtual + produtoParaAdicionar.Volume) > CaixaBase.Volume)
            {
                return false;
            }

            for (int i = 0; i < 6; i++)
            {
                var (h, l, c) = produtoParaAdicionar.GetRotatedDimensions(i);

                if (h <= CaixaBase.Altura && l <= CaixaBase.Largura && c <= CaixaBase.Comprimento)
                {
                    ProdutosEmbalados.Add(produtoParaAdicionar);
                    dimensoesUsadas = (h, l, c);
                    return true;
                }
            }
            return false;
        }
    }
}