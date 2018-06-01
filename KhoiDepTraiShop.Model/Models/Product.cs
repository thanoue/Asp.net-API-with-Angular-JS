using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KhoiDepTraiShop.Model.Abstracts;
using System.Collections.Generic;

namespace KhoiDepTraiShop.Model.Models
{
    [Table("Products")]
    public class Product :Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Alias { set; get; }

        [Required]
        public int CategoryId { set; get; }

        [MaxLength(500)]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public decimal Price { set; get; }

        public decimal? PromotionPrice { set; get; }
        public int? Warranty { set; get; }


        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }
        public virtual IEnumerable<ProductTag> ProductTags { set; get; }
        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }
        public virtual IEnumerable<ProductRating> ProductRatings { get; set; }
    }
}