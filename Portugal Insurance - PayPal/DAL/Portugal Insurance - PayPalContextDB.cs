using Portugal_Insurance___PayPal.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Portugal_Insurance___PayPal.DAL
{
    //CONSTRUCTOR DONDE SE SEÑALA LA CONEXXION CORRESPONDIENTE A LA BASE DE DATOS
    public class Portugal_Insurance___PayPalContextDB : DbContext
    {
        public Portugal_Insurance___PayPalContextDB()
           // : base("PortugalDB") //<-this was the old db that was on the hosting service
            : base("PortugalDB")
        {
 
        }

             //ESTE EVENTO EVITA QUE SE PLURALIZEN LOS NOMBRES DE LAS TABLAS
        //CREADAS APARTIR DE LOS MODELOS
        ////Fuente: http://edspencer.me.uk/2012/03/13/entity-framework-plural-and-singular-table-names/

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        //SE DECLARARAN LOS DBSETS QUE REPRESENTAN TABLAS EN LA DB
        //SEGUN LOS MODELOS DECLARADOS
        public DbSet<Precios>Precios { get; set; }

        public System.Data.Entity.DbSet<Portugal_Insurance___PayPal.Models.HomeCondoPolicy> HomeCondoPolicies { get; set; }


    }
}