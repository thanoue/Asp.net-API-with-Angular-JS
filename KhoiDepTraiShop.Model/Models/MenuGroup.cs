using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
namespace KhoiDepTraiShop.Model.Models
{
    [Table("MemuGroups")]
    public class MenuGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<Menu> Menus { get; set; }
    }
}