using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Portugal_Insurance___PayPal.Models
{
    public class Precios
    {
        [Key]
        [Display(Name = " PRICE ID")]
        public int precioID { get; set; }

        [Display(Name = " Tipo de póliza ")]
        public decimal type { get; set; }

        [Display(Name = " Tipo de Coberturas ")]
        public String coverageType { get; set; }

        [Display(Name = " Dias ")]
        public int? dias { get; set; }

        [Display(Name = " Valor Minimo ")]
        public decimal? valorMinimo { get; set; }

        [Display(Name = " Valor Maximo ")]
        public decimal? valorMaximo { get; set; }

        [Display(Name = " Total ")]
        public decimal? total { get; set; }

       

    }
}