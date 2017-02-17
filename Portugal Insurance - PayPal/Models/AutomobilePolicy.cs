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


        [Display(Name = "Policy Coverage Type ")]
        public String coverageType { get; set; }

        [Display(Name = "Policy Total Cost ")]
        public decimal? policyTotalCost { get; set; }


        [Display(Name = "PayPal Transaction Id ")]
        public string payPalTransactionId { get; set; }

        [Display(Name = "Vehicle Type")]
        public string vehicleType { get; set; }

        [Display(Name = "Vehicle Plate Number")]
        public string vehiclePlate { get; set; }

        [Display(Name = "Payment Status")]
        public String paymentStatus { get; set; }

        [Display(Name = "Payer Email")]
        public string payerEmail { get; set; }

        [Display(Name = "Payment Fee")]
        public decimal? paymentFee { get; set; }

        //To one movement correspond one user
        public String Id { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
  
        public Precios getPrice()
        {
            Portugal_Insurance___PayPalContextDB db = new Portugal_Insurance___PayPalContextDB();
            //Calculates how many days the policy lasts 
            int days = (int)this.policyEndingDate.Value.Subtract(this.policyStartingDate.Value).TotalDays;
            Precios p = db.Precios.
                Single(pre => pre.valorMinimo <= this.vehicleValue &&
                pre.valorMaximo >= this.vehicleValue &&
                pre.dias == days && 
                pre.coverageType == this.coverageType);

            return p;
        }

    }
}