using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pricing_App
{
    public class Checkout
    {
        private PricingRules _pricingRules;

        public Checkout(PricingRules pricingRules)
        {
            _pricingRules = pricingRules;
        }

        public int Total { get; set; }

        public void Scan(string item)
        {
            // go through each special rule and check if it applies
            // for e.g. is 3As present? return the rule
            // does the item contain the letter A three times?
            // if so, calculate cost, and remove the items from the list

            var rule = _pricingRules.Items.Where(pricingRule => item.Count(x => x.ToString() == pricingRule.Name) == pricingRule.SpecialPrice.Quantity);
            //Total += _pricingRules.Items.First(x => x.Name == item).UnitPrice;
        }
    }

    public class PricingRules
    {
        public List<PricingRule> Items { get; set; }
    }


    public class PricingRule
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public SpecialPrice SpecialPrice { get; set; }
    }

    public class SpecialPrice
    {
        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}
