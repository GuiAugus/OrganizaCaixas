// Data/Entities/CaixaEntity.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrganizaCaixas.Data.Entities
{
    [Table("Caixas")]    public class CaixaEntity
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? NomeCaixa { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Altura { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Largura { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Comprimento { get; set; }

    }
}