using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portugal_Insurance___PayPal.Models.ViewModels
{
    public class VMAutoPolicyInfo
    {
            public String vinNumber { get; set; }
            public decimal vehicleValue { get; set; }
            public DateTime startingDate { get; set; }
            public DateTime endingDate { get; set; }
            public int? diasDeCobertura { get; set; }
            public int carYears { get; set; }
            public String carMakes { get; set; }

            public String carModels { get; set; }

            public String coverageType { get; set; }

            public decimal policyTotal { get; set; }
            public String email { get; set;
            }


    }
}