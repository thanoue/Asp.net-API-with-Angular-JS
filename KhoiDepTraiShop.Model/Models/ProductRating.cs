using System;
using KhoiDepTraiShop.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KhoiDepTraiShop.Model.Models
{
    [Table("ProductRatings")]
    public class ProductRating
    {


        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //public int Id { set; get; }

        [Required]
        public int RatedProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime? RatingTime { set; get; }

        [StringLength(1000)]
        [MinLength(15)]
        [Required]
        public string RatingContent { set; get; }

        [StringLength(30)]
        public string RatingTitle { set; get; }

        public int? RatingScore { set; get; }

        public DateTime? PublicDate { set; get; }

        [Required]
        public ProductRatingStatus Status { get; set; }

        [ForeignKey("RatedProductId")]
        public virtual  Product Product { set; get; }




    }
}
