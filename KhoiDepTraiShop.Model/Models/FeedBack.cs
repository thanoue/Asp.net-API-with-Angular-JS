using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhoiDepTraiShop.Common;

namespace KhoiDepTraiShop.Model.Models
{
[   Table("FeedBacks")]
    public class FeedBack
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Subject { get; set; } 
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [MinLength(20)]
        public string Message { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public FeedBackStatus Status { get; set; }
    }
}
