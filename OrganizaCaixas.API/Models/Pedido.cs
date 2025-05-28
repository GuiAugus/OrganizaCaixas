// Models/Pedido.cs
using System;
using System.Collections.Generic;

namespace OrganizaCaixas.Models
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public List<ProdutoComQuantidade> Produtos { get; set; }

        public Pedido(Guid id)
        {
            Id = id;
            Produtos = new List<ProdutoComQuantidade>();
        }
    }

    public class ProdutoComQuantidade
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public ProdutoComQuantidade(Produto produto, int quantidade)
        {
            Produto = produto;
            Quantidade = quantidade;
        }
    }
}