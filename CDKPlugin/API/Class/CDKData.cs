using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CDKPlugin.API.Class
{
    public class CDKData
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("")]
        [StringLength(64)]
        public string CKey { get; set; } = String.Empty;
    }
}