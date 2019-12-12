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
            var applicableRules = _pricingRules.Items.Where(x => GetMatchingRule(x, item));

            Total = applicableRules.Sum(x =>
                x.SpecialPrice.Price * item.Count(y => y.ToString() == x.Name) /
                x.SpecialPrice
                    .Quantity) + RemoveItems(applicableRules.ToList(), item).Sum(x => _pricingRules.Items.First(y => y.Name == x.ToString()).UnitPrice);
        }

        public string RemoveItems(List<PricingRule> rules, string item)
        {
            var items = string.Concat(item.OrderBy(x => x)).ToList();

            foreach (var rule in rules)
            {
                items.RemoveRange(items.IndexOf(rule.Name.ToCharArray().First()), rule.SpecialPrice.Quantity * item.Count(x => x.ToString() == rule.Name) / rule.SpecialPrice.Quantity);
            }

            return string.Concat(items);
        }

        public bool GetMatchingRule(PricingRule pricingRule, string item)
        {
            return item.Count(x => x.ToString() == pricingRule.Name) >= pricingRule.SpecialPrice.Quantity;
        }
    }

}
