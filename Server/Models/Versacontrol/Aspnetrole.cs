using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("aspnetroles")]
    public partial class Aspnetrole
    {
        [Key]
        [Required]
        public string Id { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string Name { get; set; }

        public string NormalizedName { get; set; }

        public ICollection<Aspnetroleclaim> Aspnetroleclaims { get; set; }

        public ICollection<Aspnetuserrole> Aspnetuserroles { get; set; }
    }
}