using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace Portugal_Insurance___PayPal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Full Name")]
        public String fullName { get; set; }

        [Display(Name = "address Line1")]
        public String addressLine1 { get; set; }
        [Display(Name = "address Line1")]
        public String addressLine2 { get; set; }

        [Display(Name = "City")]
        public String city { get; set; }

        [Display(Name = "State")]
        public String state { get; set; }

        [Display(Name = "Zip Code")]
        public String zipCode { get; set; }

        [Display(Name = "Country")]
        public String country { get; set; }

        [Display(Name = "Phone Number")]
        public String phoneNumber { get; set; }

        [Display(Name = "Email Address")]
        public String emailAddress { get; set; }

        [Display(Name = "Lincense Number 1")]
        public String licenseNumber1 { get; set; }

        [Display(Name = "Lincense Number 2")]
        public String licenseNumber2 { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PortugalDB")
        {
        }
    }
}