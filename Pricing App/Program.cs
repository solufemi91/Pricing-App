using System;
using System.Linq;
using System.Collections.Generic;

namespace Pricing_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pricingRules = new PricingRules
            {
                Items = new List<PricingRule>
                {
                    new PricingRule
                    {
                        Name = "A",
                        SpecialPrice = new SpecialPrice
                        {
                            Price = 130,
                            Quantity = 3
                        },
                        UnitPrice = 50
                    },
                    new PricingRule
                    {
                        Name = "B",
                        SpecialPrice = new SpecialPrice
                        {
                            Price = 45,
                            Quantity = 2
                        },
                        UnitPrice = 30
                    },
                    new PricingRule
                    {
                        Name = "C",
                        UnitPrice = 20
                    }
                }
               
            };

            var checkout = new Checkout(pricingRules);

            var itemsList = "AAABB";
            checkout.Scan(itemsList);

            //foreach (var item in itemsList)
            //{
            //    checkout.Scan(item.ToString());
            //}

            var price = checkout.Total;
        }
    }
}
