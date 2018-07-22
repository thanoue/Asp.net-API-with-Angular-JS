using KhoiDepTraiShop.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhoiDepTraiShop.Model.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [StringLength(128)]       
        public string CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        [StringLength(256)]
        public string CustomerName { set; get; }

        [Required]
        [StringLength(256)]
        public string CustomerAddress { set; get; }

        [Required]
        [StringLength(256)]
        public string CustomerEmail { set; get; }

        [Required]
        [StringLength(50)]
        public string CustomerMobile { set; get; }

        [StringLength(256)]
        public string CustomerMessage { set; get; }

        public PaymentMethod PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public bool Deleted { set; get; }

        public OrderStatus Status { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }

    }
}
