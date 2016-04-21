using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowTrello.Models;

namespace PowTrello.DAL
{
    internal sealed class ConfigurationDb : DbMigrationsConfiguration<DTContext>
    {
        public ConfigurationDb()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            ContextKey = "PowTrello.DAL.DTContext";
        }

        protected override void Seed(DTContext context)
        {
            CreateTestRecord(context);
        }

        public void CreateTestRecord(DTContext context)
        {
            //var firstEntry = context.CurrencyInformations.ToList();
            //if (firstEntry.Count == 0)
            //{
            //    context.CurrencyInformations.AddOrUpdate(newTestEntry => newTestEntry.Id, new CurrencyInformation()
            //    {
            //        CodeOfCurrency = "YYY",
            //        CreatedAt = DateTime.Now,
            //        DateFromFile = DateTime.Now.AddDays(20),
            //        TheValueOfTheExchangeRate = 3.33M

            //    });
            //}
        }
    }
}
