using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using KhoiDepTraiShop.Common;
using System.ComponentModel.DataAnnotations.Schema;
namespace KhoiDepTraiShop.Model.Models
{
    [Table("Tags")]
    public class Tag
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Id { set; get; }

        [MaxLength(50)]
        [Required]
        public string Name { set; get; }


        [Required]
        public TagType Type { set; get; }
    }
}
