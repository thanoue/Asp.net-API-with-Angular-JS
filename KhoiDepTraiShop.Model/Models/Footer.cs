using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoiDepTraiShop.Model.Models
{
    [Table("Footers")]
    public class Footer
    {
        [Key]
        public string Id { set; get; }

        [Required]
        public string Content { get; set; }
    }
}