using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace OrganizaCaixas.Data.Entities
{
    [Table("PedidosProcessados")]
    public class PedidoProcessadoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PedidoIdOriginal { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string ResultadoCaixasJson { get; set; } = string.Empty;
        public DateTime DataProcessamento { get; set; } = DateTime.Now;
    }
}