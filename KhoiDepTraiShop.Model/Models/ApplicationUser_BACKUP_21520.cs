using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Model.Models
{
    public class ApplicationUser : IdentityUser
    {

        [StringLength(256)]
        public string FullName { get; set; }

        [StringLength(256)] 
        public string Address { get; set; }

        public DateTime? BirthDay { get; set; }


        [StringLength(256)]
        public string CridentialCode { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {//quản lý identity thông qua cookie
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this,authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
        public virtual ICollection< ProductRating> ProductRatings { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}