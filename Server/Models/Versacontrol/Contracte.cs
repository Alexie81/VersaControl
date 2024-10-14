using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("contracte")]
    public partial class Contracte
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("Id_Beneficiar")]
        [Required]
        public int IdBeneficiar { get; set; }

        public Beneficiari Beneficiari { get; set; }

        [Column("Id_Furnizor")]
        [Required]
        public int IdFurnizor { get; set; }

        public Contractori Contractori { get; set; }

        [Column("Scop Contract")]
        [Required]
        public string ScopContract { get; set; }

        [Column("Suma Contract")]
        [Required]
        public float SumaContract { get; set; }

        [Column("Id_Moneda")]
        [Required]
        public int IdMoneda { get; set; }

        public Monede Monede { get; set; }

        [Column("Id_TipContract")]
        [Required]
        public int IdTipContract { get; set; }

        public TipuriContract TipuriContract { get; set; }

        public ICollection<Anexa> Anexas { get; set; }
    }
}