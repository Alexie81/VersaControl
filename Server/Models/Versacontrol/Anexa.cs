using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("anexa")]
    public partial class Anexa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_contract")]
        [Required]
        public int IdContract { get; set; }

        public Contracte Contracte { get; set; }

        [Column("scop_anexa")]
        [Required]
        public string ScopAnexa { get; set; }

        [Column("suma_anexa")]
        [Required]
        public string SumaAnexa { get; set; }

        [Column("Id_Moneda")]
        [Required]
        public int IdMoneda { get; set; }

        public Monede Monede { get; set; }
    }
}