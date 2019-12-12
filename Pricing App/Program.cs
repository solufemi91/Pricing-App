using System;
using System.Linq;
using System.Collections.Generic;
using Pricing_App.Dtos;

namespace Pricing_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pricingRules = GetPricingRules();

            var itemsList = "ABABA";

            var checkout = new Checkout(pricingRules);

            checkout.Scan(itemsList);

            var price = checkout.Total;
        }

        public static PricingRules GetPricingRules()
        {
            return new PricingRules
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
                        SpecialPrice = new SpecialPrice
                        {
                            Price = 130,
                            Quantity = 3
                        },
                        UnitPrice = 20
                    }
                }

            };
        }
    }
}
