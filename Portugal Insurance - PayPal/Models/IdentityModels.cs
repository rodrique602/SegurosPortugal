using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Portugal_Insurance___PayPal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

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

        [Display(Name = "Email Address")]
        public String emailAddress { get; set; }

        [Display(Name = "Lincense Number 1")]
        public String licenseNumber1 { get; set; }

        [Display(Name = "Lincense Number 2")]
        public String licenseNumber2 { get; set; }

        public virtual ICollection<AutomobilePolicy> AutomobilePolicies { get; set; }

    }

    public class Portugal_Insurance___PayPalContextDB : IdentityDbContext<ApplicationUser>
    {
        public Portugal_Insurance___PayPalContextDB()
            : base("PortugalDB", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userClaims = modelBuilder.Entity<IdentityUserClaim>()
                .ToTable("AspNetUserClaims");
            userClaims.Property(u => u.UserId).HasColumnName("User_Id");
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var userClaims = modelBuilder.Entity<IdentityUserClaim>()
        //        .ToTable("AspNetUserClaims");
        //    userClaims.Property(u => u.UserId).HasColumnName("Id");
        //}

        public static Portugal_Insurance___PayPalContextDB Create()
        {
            return new Portugal_Insurance___PayPalContextDB();
        }

        //Entidades definidas para atender requerimientos de logica de negocio
        //public DbSet<AutomobilePolicy> AutomobilePolicies { get; set; }
        //SE DECLARARAN LOS DBSETS QUE REPRESENTAN TABLAS EN LA DB
        //SEGUN LOS MODELOS DECLARADOS
        public virtual DbSet<Precios> Precios { get; set; }

        public virtual DbSet<HomeCondoPolicy> HomeCondoPolicies { get; set; }
        public virtual DbSet<AutomobilePolicy> AutomobilePolicies { get; set; }


    }
}