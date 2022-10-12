using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public decimal GetUnitaryTax()
        {
            return Round((Price / 100m) * Category.GetTaxPercent());
        }
        public decimal GetUnitaryTaxedAmount()
        {
            return Round(Price + GetUnitaryTax());
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }
}