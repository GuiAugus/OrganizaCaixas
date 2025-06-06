using OrganizaCaixas.Dtos.Input;
using OrganizaCaixas.Dtos.Output;
using OrganizaCaixas.Models;
using OrganizaCaixas.Data; 
using OrganizaCaixas.Data.Entities; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace OrganizaCaixas.Services
{
    public class PackagingService : IPackagingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly List<Caixa> _caixasDisponiveisCache;

        public PackagingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _caixasDisponiveisCache = _dbContext.Caixas
                                        .Select(ce => new Caixa(ce.NomeCaixa!, ce.Altura, ce.Largura, ce.Comprimento))
                                        .ToList();
        }

        public async Task<PedidosWrapperOutputDto> ProcessarPedidosAsync(PedidosWrapperInputDto pedidosWrapperInput)
        {
            await Task.FromResult(0);

            var resultadosPedidos = new List<PedidoResponseOutputDto>();

            foreach (var pedidoInput in pedidosWrapperInput.Pedidos)
            {
                var pedidoModel = MapearPedidoInputParaModelo(pedidoInput);
                var caixasDoPedido = ExecutarEmpacotamento(pedidoModel);
                var pedidoOutputDto = MapearResultadoParaPedidoOutputDto(pedidoInput.PedidoId, caixasDoPedido);

                var PedidoProcessadoEntity = new PedidoProcessadoEntity
                {
                    PedidoIdOriginal = pedidoOutputDto.PedidoId,
                    ResultadoCaixasJson = JsonSerializer.Serialize(pedidoOutputDto),
                    DataProcessamento = DateTime.UtcNow
                };
                _dbContext.PedidosProcessados.Add(PedidoProcessadoEntity);

                resultadosPedidos.Add(pedidoOutputDto);
            }

            await _dbContext.SaveChangesAsync();

            return new PedidosWrapperOutputDto { Pedidos = resultadosPedidos };
        }

        private Pedido MapearPedidoInputParaModelo(PedidoRequestInputDto pedidoInput)
        {
            var pedido = new Pedido(Guid.NewGuid());

            foreach (var produtoInput in pedidoInput.Produtos)
            {
                var produto = new Produto(
                    produtoInput.ProdutoId!,
                    produtoInput.ProdutoId!,
                    produtoInput.Dimensoes!.Altura,
                    produtoInput.Dimensoes!.Largura,
                    produtoInput.Dimensoes!.Comprimento
                );
                pedido.Produtos.Add(new ProdutoComQuantidade(produto, 1));
            }
            return pedido;
        }

        private List<CaixaAtual> ExecutarEmpacotamento(Pedido pedido)
        {
            var caixasUtilizadasNestePedido = new List<CaixaAtual>();

            var todosProdutosIndividuais = pedido.Produtos
                .SelectMany(pcq => Enumerable.Range(0, pcq.Quantidade).Select(_ => pcq.Produto))
                .ToList();

            todosProdutosIndividuais = todosProdutosIndividuais
                .OrderByDescending(p => p.Volume)
                .ToList();

            foreach (var produtoParaEmbalar in todosProdutosIndividuais)
            {
                bool produtoEmbalado = false;
                (decimal h, decimal l, decimal c) dimensoesUsadasNaCaixa = (0, 0, 0);

                foreach (var caixaAtual in caixasUtilizadasNestePedido)
                {
                    if (caixaAtual.TentarAdicionarProduto(produtoParaEmbalar, out dimensoesUsadasNaCaixa))
                    {
                        produtoEmbalado = true;
                        break;
                    }
                }

                if (!produtoEmbalado)
                {
                    Caixa? caixaBaseAdequada = null;

                    foreach (var caixaTipo in _caixasDisponiveisCache.OrderBy(c => c.Volume))
                    {
                        if (ProdutoCabeNaCaixa(produtoParaEmbalar, caixaTipo))
                        {
                            caixaBaseAdequada = caixaTipo;
                            break;
                        }
                    }

                    if (caixaBaseAdequada != null)
                    {
                        var novaCaixa = new CaixaAtual(caixaBaseAdequada);
                        novaCaixa.TentarAdicionarProduto(produtoParaEmbalar, out dimensoesUsadasNaCaixa);
                        caixasUtilizadasNestePedido.Add(novaCaixa);
                        produtoEmbalado = true;
                    }
                    else
                    {
                        var caixaParaProdutoNaoEmbalavel = new CaixaAtual(new Caixa("Nao Aplicavel", 0, 0, 0));
                        caixaParaProdutoNaoEmbalavel.ProdutosEmbalados.Add(produtoParaEmbalar);
                        caixasUtilizadasNestePedido.Add(caixaParaProdutoNaoEmbalavel);
                        produtoEmbalado = true;
                    }
                }
            }

            return caixasUtilizadasNestePedido;
        }

        private bool ProdutoCabeNaCaixa(Produto produto, Caixa caixa)
        {
            for (int i = 0; i < 6; i++)
            {
                var (h, l, c) = produto.GetRotatedDimensions(i);

                if (h <= caixa.Altura && l <= caixa.Largura && c <= caixa.Comprimento)
                {
                    return true;
                }
            }
            return false;
        }

        private PedidoResponseOutputDto MapearResultadoParaPedidoOutputDto(int pedidoIdOriginal, List<CaixaAtual> caixasUtilizadasInternas)
        {
            var outputDto = new PedidoResponseOutputDto
            {
                PedidoId = pedidoIdOriginal,
                Caixas = new List<CaixaOutputDto>()
            };

            foreach (var caixaAtual in caixasUtilizadasInternas)
            {
                var caixaDto = new CaixaOutputDto
                {
                    Produtos = new List<string>()
                };

                if (caixaAtual.CaixaBase.NomeCaixa == "Nao Aplicavel")
                {
                    caixaDto.CaixaId = null;
                    caixaDto.Observacao = "Produto não cabe em nenhuma caixa disponível.";
                    foreach (var produtoNaoEmbalado in caixaAtual.ProdutosEmbalados)
                    {
                        caixaDto.Produtos.Add(produtoNaoEmbalado.Id);
                    }
                }
                else
                {
                    caixaDto.CaixaId = caixaAtual.CaixaBase.NomeCaixa;
                    foreach (var produtoEmbalado in caixaAtual.ProdutosEmbalados)
                    {
                        caixaDto.Produtos.Add(produtoEmbalado.Id);
                    }
                }
                outputDto.Caixas.Add(caixaDto);
            }

            return outputDto;
        }

        public async Task<List<PedidoResponseOutputDto>> GetOrderHistoryAsync()
        {
            var entities = await _dbContext.PedidosProcessados.ToListAsync();

            var results = new List<PedidoResponseOutputDto>();
            foreach (var entity in entities)
            {
                var pedidoOutputDto = JsonSerializer.Deserialize<PedidoResponseOutputDto>(entity.ResultadoCaixasJson);
                if (pedidoOutputDto != null)
                {
                    results.Add(pedidoOutputDto);
                }
            }
            return results;
        }

        public async Task<PedidoResponseOutputDto?> GetOrderHistoryByIdAsync(int pedidoIdOriginal)
        {
            var entity = await _dbContext.PedidosProcessados
                                        .FirstOrDefaultAsync(p => p.PedidoIdOriginal == pedidoIdOriginal);

            if (entity == null)
            {
                return null; 
            }

            var pedidoOutputDto = JsonSerializer.Deserialize<PedidoResponseOutputDto>(entity.ResultadoCaixasJson);
            return pedidoOutputDto;
        }
    }
}