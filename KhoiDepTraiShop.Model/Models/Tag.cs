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
        [StringLength(50)]
        public string Id { set; get; }

        [StringLength(50)]
        [Required]
        public string Name { set; get; }

        [Required]
        public TagType Type { set; get; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
