using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portugal_Insurance___PayPal.Models
{
    public class HomeCondoPolicy
    {
        [Key]
        [Display(Name = " Policy Quote ID # ")]
        public int policyQuoteID { get; set; }

        [Display(Name = " Home Owner Insurance")]
        public bool? homeOwnerInsurance { get; set; }

        [Display(Name = " Condo Insurance")]
        public bool? condoInsurance { get; set; }

        [Display(Name = " Name ")]
        public String customerName { get; set; }

        [Display(Name = " Email ")]
        public String email { get; set; }

        [Display(Name = " U.S.A. Address ")]
        public String customerUsaAddress { get; set; }

        [Display(Name = " Zip Code ")]
        public double? zipCode { get; set; }

        [Display(Name = " City ")]
        public String city { get; set; }

        [Display(Name = " Phone U.S.A ")]
        public String phoneUsa { get; set; }

        [Display(Name = " Cell Phone ")]
        public String cellPhone { get; set; }

        [Display(Name = " Insured Building Location ")]
        public String insuredBuildingLocation { get; set; }

        [Display(Name = " Floors ")]
        public int? floors { get; set; }

        [Display(Name = " Type of Construction")]
        public String typeOfConstruction { get; set; }

        [Display(Name = " Wall")]
        public String wall { get; set; }

        [Display(Name = " Roof")]
        public String roof { get; set; }

        [Display(Name = " Building's Value ")]
        public decimal? buildingsValue { get; set; }

        [Display(Name = " Content's Value ")]
        public decimal? contentsValue { get; set; }

        [Display(Name = " Burglary / Theft")]
        public String burlglaryTheft { get; set; }

        [Display(Name = " Broken Glass ")]
        public String brokenGlass { get; set; }

        [Display(Name = " Family Civil Liability ")]
        public String familyCivilLiability { get; set; }

        [Display(Name = " OTHERS: ")]
        public String others { get; set; }

        [Display(Name = " Annual Premium ")]
        public decimal? annualPremium { get; set; }

        [Display(Name = " EarthQuake(optional) ")]
        public bool? earthquake { get; set; }


    }
}