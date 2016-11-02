using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity;

namespace Portugal_Insurance___PayPal.Models
{
    public class AutomobilePolicy
    {
        [Key]
        [Display(Name = "Policy ID")]
        public int automobilePolicyID { get; set; }

        [Display(Name = "Vehicle Value")]
        public int? vehicleValue { get; set; }

        [Display(Name = "Vehicle VIN")]
        public String vehicleVin { get; set; }

        [Display(Name = "Car Year")]
        public String carYear { get; set; }

        [Display(Name = "Car Make")]
        public String carMake { get; set; }

        [Display(Name = "Car Model")]
        public String carModel { get; set; }

        [Display(Name = "Policy Folio")]
        public String policyFolio { get; set; }

        [Display(Name = "Policy Sold")]
        public Boolean? policySold { get; set; }

        [Display(Name = "Policy Sold Date")]
        public DateTime? policySoldDate { get; set; }

        [Display(Name = "Policy Starting Date")]
        public DateTime? policyStartingDate { get; set; }

        [Display(Name = "Policy Ending Date")]
        public DateTime? policyEndingDate { get; set; }

        //To one movement correspond one user
        public String Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }



    }
}