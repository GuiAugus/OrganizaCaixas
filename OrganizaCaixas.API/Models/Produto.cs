namespace OrganizaCaixas.Models
{
    public class Produto
    {
        public string Id { get; set; }
        public string NomeProduto { get; set; }
        public decimal Altura { get; set; }
        public decimal Largura { get; set; }
        public decimal Comprimento { get; set; }

        public decimal Volume => Altura * Largura * Comprimento;

        public Produto(string id, string nomeproduto, decimal altura, decimal largura, decimal comprimento)
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
        public (decimal h, decimal l, decimal c) GetRotatedDimensions(int rotationIndex)
        {
            return rotationIndex switch
            {
                0 => (Altura, Largura, Comprimento),
                1 => (Altura, Comprimento, Largura),
                2 => (Largura, Altura, Comprimento),
                3 => (Largura, Comprimento, Altura),
                4 => (Comprimento, Altura, Largura),
                5 => (Comprimento, Largura, Altura),
                _ => throw new ArgumentOutOfRangeException(nameof(rotationIndex), "Indice de rotacao invalido. Deve ser entre 0 e 5.")
            };
        }
    }
}