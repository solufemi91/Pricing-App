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

        public void Scan(string basketItems)
        {
            var applicableRules = _pricingRules.Items
                .Where(x => IsMatchingRule(x, basketItems));

            var totalPriceOfSpecialPriceItems = applicableRules
                .Sum(x => x.SpecialPrice.Price * QuantityOfSpecialPricesGroups(x, basketItems));

            var nonSpecialPriceItems = GetNonSpecialPriceItems(applicableRules, basketItems);

            var totalPriceOfNonSpecialPriceItems = nonSpecialPriceItems
                .Sum(x => _pricingRules.Items.First(y => y.Name == x.ToString()).UnitPrice);

            Total = totalPriceOfSpecialPriceItems + totalPriceOfNonSpecialPriceItems;
        }

        public string GetNonSpecialPriceItems(IEnumerable<PricingRule> rules, string basketItems)
        {
            var orderedBasketItems = string.Concat(basketItems.OrderBy(x => x)).ToList();

            foreach (var rule in rules)
            {
                var count = rule.SpecialPrice.Quantity * QuantityOfSpecialPricesGroups(rule, basketItems);
                var startIndex = orderedBasketItems.IndexOf(char.Parse(rule.Name));

                orderedBasketItems.RemoveRange(startIndex, count);
            }
            return string.Concat(orderedBasketItems);
        }

        public int QuantityOfSpecialPricesGroups(PricingRule rule, string item)
        {
            return item.Count(x => x.ToString() == rule.Name) / rule.SpecialPrice.Quantity;
        }

        public bool IsMatchingRule(PricingRule pricingRule, string item)
        {
            return item.Count(x => x.ToString() == pricingRule.Name) >= pricingRule.SpecialPrice.Quantity;
        }
    }

}
