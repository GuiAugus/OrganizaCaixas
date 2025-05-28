namespace OrganizaCaixas.Models
{
    public class Caixa
    {
        public string NomeCaixa { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public decimal Volume => Altura * Largura * Comprimento;

        public Caixa(string nome, decimal altura, decimal largura, decimal comprimento)
        {
            NomeCaixa = nome;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }

        public override string ToString()
        {
            return $"{NomeCaixa}: ({Altura} * {Largura} * {Comprimento})";
        }
    }
}