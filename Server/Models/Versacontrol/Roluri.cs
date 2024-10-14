using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("roluri")]
    public partial class Roluri
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("nume")]
        [Required]
        public string Nume { get; set; }

        public ICollection<Beneficiari> Beneficiaris { get; set; }

        public ICollection<Contractori> Contractoris { get; set; }
    }
}