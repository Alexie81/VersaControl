using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("__efmigrationshistory")]
    public partial class Efmigrationshistory
    {
        [Key]
        [Required]
        public string MigrationId { get; set; }

        [Required]
        public string ProductVersion { get; set; }
    }
}