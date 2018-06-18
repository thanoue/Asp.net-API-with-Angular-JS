using KhoiDepTraiShop.Common;
using KhoiDepTraiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KhoiDepTraiShop.Web.Models
{
    public class FeedBackViewModel
    {
        public FeedBackViewModel() { }

        public int Id { get; set; }

        [Required(ErrorMessage = "Phải nhập tên")]
        [MaxLength(250, ErrorMessage = "Tên không được quá 250 ký tự")]
        public string Name { get; set; } = "";

        public string Subject { get; set; } = "";

        [Required(ErrorMessage = "Phải nhập Email")]
        [MaxLength(250, ErrorMessage = "Email không được quá 250 ký tự")]
        [Email]
        public string Email { get; set; } = "";

        [MinLength(20, ErrorMessage = "Lời nhắn không được ít hơn 20 ký tự")]
        [Required(ErrorMessage = "Phải nhập nội dung")]
        public string Message { get; set; } = "";


        public DateTime CreateDate { get; set; } = DateTime.Now;


        public FeedBackStatus Status { get; set; } = FeedBackStatus.Wating;
    }

    public static class FeedBackViewModelEmm
    {
        public static FeedBackViewModel ToViewModel(this FeedBack entity)
        {
            var vm = new FeedBackViewModel()
            {
                CreateDate = entity.CreateDate,
                Email = entity.Email,
                Id = entity.Id,
                Message = entity.Message,
                Name = entity.Name,
                Status = entity.Status,
                Subject = entity.Subject
            };
            return vm;
        }

        public static List<FeedBackViewModel> ToViewModelList(this IList<FeedBack> entities)
        {
            var vm = new List<FeedBackViewModel>();
            vm.AddRange(entities.Select(p => p.ToViewModel()));
            return vm;
        }

        public static FeedBack Toentity(this FeedBackViewModel entity)
        {
            var vm = new FeedBack()
            {
                CreateDate = entity.CreateDate,
                Email = entity.Email,
                Id = entity.Id,
                Message = entity.Message,
                Name = entity.Name,
                Status = entity.Status,
                Subject = entity.Subject
            };
            return vm;
        }

        public static List<FeedBack> ToEntityList(this IList<FeedBackViewModel> entities)
        {
            var vm = new List<FeedBack>();
            vm.AddRange(entities.Select(p => p.Toentity()));
            return vm;
        }
    }

    public class EmailAttribute : RegularExpressionAttribute
    {
        public EmailAttribute()
            : base(@"[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}")
        {
            this.ErrorMessage = "Email không hợp lệ";
        }
    }
}