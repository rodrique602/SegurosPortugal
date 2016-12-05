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
            public String email { get; set;  }

        public VMAutoPolicyInfo() { }

        public VMAutoPolicyInfo(String[] customData)
        {
            this.vehicleValue = int.Parse(customData[0]);
            this.vinNumber = customData[1].ToString();
            this.carYears = int.Parse(customData[2].ToString());
            this.carMakes = customData[3].ToString();
            this.carModels = customData[4].ToString();
            this.startingDate = DateTime.Parse(customData[5]);
            this.endingDate = DateTime.Parse(customData[6]);
            //this.coverageType = customData[7];
        }


    }
}