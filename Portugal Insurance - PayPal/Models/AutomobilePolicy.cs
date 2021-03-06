﻿using System;
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

        [Display(Name = "Type")]
        public string vehicleType { get; set; }

        [Display(Name = "VIN #")]
        public String vehicleVin { get; set; }

        [Display(Name = "Year")]
        public String carYear { get; set; }

        [Display(Name = "Make")]
        public String carMake { get; set; }

        [Display(Name = "Model")]
        public String carModel { get; set; }

        [Display(Name = "Plate Number")]
        public string vehiclePlate { get; set; }

        [Display(Name = "Policy Folio")]
        public String policyFolio { get; set; }

        [Display(Name = "Policy Sold")]
        public Boolean? policySold { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Policy Sold Date")]
        public DateTime? policySoldDate { get; set; }

        [Display(Name = "Coverage Days")]
        public int? policyCoverageDays { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Starting Date")]
        public DateTime? policyStartingDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ending Date")]
        public DateTime? policyEndingDate { get; set; }

        [Display(Name = "Coverage Type ")]
        public String coverageType { get; set; }

        [Display(Name = "Total")]
        public decimal? policyTotalCost { get; set; }


        [Display(Name = "PayPal Transaction Id ")]
        public string payPalTransactionId { get; set; }

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