namespace OrganizaCaixas.Models
{
    public class Produto
    {
        public Guid Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public decimal Volume => Altura * Largura * Comprimento;

        public Produto(Guid id, string nomeproduto, decimal altura, decimal largura, decimal comprimento)
        {
            Id = id;
            NomeProduto = nomeproduto;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        } 

        public override string ToString()
        {
            return $"{NomeProduto}: ({Altura} x {Largura} x {Comprimento})";
        }
    }
}