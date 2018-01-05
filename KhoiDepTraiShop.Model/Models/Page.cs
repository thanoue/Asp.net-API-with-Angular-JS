using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KhoiDepTraiShop.Model.Abstracts;
namespace KhoiDepTraiShop.Model.Models
{
    [Table("Pages")]
    public class Page : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        [Required]
        public string Alias { set; get; }

        public string Content { set; get; }
    }
}
