using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("monede")]
    public partial class Monede
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("Prescurtare Moneda")]
        [Required]
        public string PrescurtareMoneda { get; set; }

        public ICollection<Anexa> Anexas { get; set; }

        public ICollection<Contracte> Contractes { get; set; }
    }
}