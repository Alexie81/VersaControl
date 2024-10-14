using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("beneficiari")]
    public partial class Beneficiari
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("Nume Companie")]
        [Required]
        public string NumeCompanie { get; set; }

        [Column("CUI")]
        [Required]
        public string Cui { get; set; }

        [Column("Adresa Mail Office")]
        [Required]
        public string AdresaMailOffice { get; set; }

        [Required]
        public string Adresa { get; set; }

        [Required]
        public string Reprezentant { get; set; }

        [Column("Nr Telefon")]
        [Required]
        public int NrTelefon { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int Rol { get; set; }

        public Roluri Roluri { get; set; }

        public ICollection<Contracte> Contractes { get; set; }
    }
}