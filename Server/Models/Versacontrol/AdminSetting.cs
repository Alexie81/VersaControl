using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VersaControl.Server.Models.versacontrol
{
    [Table("admin_settings")]
    public partial class AdminSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("Path For Saving Documents")]
        [Required]
        public string PathForSavingDocuments { get; set; }

        [Column("Ftp Host Name")]
        [Required]
        public string FtpHostName { get; set; }

        [Column("Ftp UserName")]
        [Required]
        public string FtpUserName { get; set; }

        [Column("Ftp Password")]
        [Required]
        public string FtpPassword { get; set; }

        [Column("Ftp Port")]
        [Required]
        public int FtpPort { get; set; }
    }
}