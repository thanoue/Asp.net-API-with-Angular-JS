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
    [Table("AddressProvinces")]
    public class AddressProvince
    {
       
        [Key]
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public int SortOrder { get; set; }
        
    }
}
