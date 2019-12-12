namespace Pricing_App.Dtos
{

    public class PricingRule
    {
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public SpecialPrice SpecialPrice { get; set; }
    }
}
