using System;
using KhoiDepTraiShop.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KhoiDepTraiShop.Model.Models
{
    [Table("ProductRatings")]
    public class ProductRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public DateTime? RatingTime { set; get; }

        [Required]
        public int ProductId { get; set; }

        [MaxLength(1000)]
        [MinLength(15)]
        [Required]
        public string RatingContent { set; get; }

        public int? RatingScore { set; get; }

        [Required]
        public string RatingPeopleName { set; get;}

        
        public DateTime? PublicDate { set; get; }

        [Required]
        public ProductRatingStatus Status { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

    }
}
