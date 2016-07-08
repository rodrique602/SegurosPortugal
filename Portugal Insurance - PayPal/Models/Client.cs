using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portugal_Insurance___PayPal.Models
{
    public class Client
    {

    [Key]
    [Display(Name="Client ID")]
     public int clientID { get; set; }

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


        //Un cliente puede tener muchas polizas
    public virtual ICollection<AutomobilePolicy> AutomobilePolicies { get; set; }

    }
}