using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Pricing_App.Dtos;

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
            var applicableRules = _pricingRules.Items.Where(x => IsMatchingRule(x, item));

            var totalPriceOfSpecialPriceItems = applicableRules.Sum(x => x.SpecialPrice.Price * (item.Count(y => y.ToString() == x.Name)/x.SpecialPrice.Quantity));

            var totalPriceOfNonSpecialPriceItems = GetNonSpecialPriceItems(applicableRules.ToList(), item).Sum(x => _pricingRules.Items.First(y => y.Name == x.ToString()).UnitPrice);

            Total = totalPriceOfSpecialPriceItems + totalPriceOfNonSpecialPriceItems;
        }

        public string GetNonSpecialPriceItems(List<PricingRule> rules, string item)
        {
            var items = string.Concat(item.OrderBy(x => x)).ToList();

            foreach (var rule in rules)
            {
                var count = rule.SpecialPrice.Quantity * (item.Count(x => x.ToString() == rule.Name)/rule.SpecialPrice.Quantity);
                var startIndex = items.IndexOf(rule.Name.ToCharArray().First());

                items.RemoveRange(startIndex, count);
            }

            return string.Concat(items);
        }

        //public int QuantityOfSpecialPricesGroups(List<PricingRule> rules)
        //{
        //    item.Count(x => x.ToString() == rule.Name) / rule.SpecialPrice.Quantity
        //}

        public bool IsMatchingRule(PricingRule pricingRule, string item)
        {
            return item.Count(x => x.ToString() == pricingRule.Name) >= pricingRule.SpecialPrice.Quantity;
        }
    }

}
