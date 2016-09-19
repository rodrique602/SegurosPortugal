using System;
using System.ComponentModel.DataAnnotations;

namespace Portugal_Insurance___PayPal.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contaseña Actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contaseña Nueva")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Nueva Contaseña")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contaseña")]
        public string Password { get; set; }

        [Display(Name = "Recuerdame")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contaseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contaseña")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public String fullName { get; set; }

        [Required]
        [Display(Name = "address Line1")]
        public String addressLine1 { get; set; }
        [Display(Name = "address Line1")]
        public String addressLine2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public String city { get; set; }

        [Required]
        [Display(Name = "State")]
        public String state { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public String zipCode { get; set; }

        [Required]
        [Display(Name = "Country")]
        public String country { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public String phoneNumber { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public String emailAddress { get; set; }

        [Required]
        [Display(Name = "Lincense Number 1")]
        public String licenseNumber1 { get; set; }

        [Display(Name = "Lincense Number 2")]
        public String licenseNumber2 { get; set; }



        ////Default validation error messages
        //public static class AccountRolesNames
        //{
        //    public const string ADMINISTRATOR = "Administrador";
        //    public const string SALES_MANAGER = "SalesManager";
        //    public const string CLIENT = "Client";


        //}
    }
}
