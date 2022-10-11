using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public decimal GetUnitaryTax()
        {
            return (Price / 100m) * Category.TaxPercentage;
        }
        public decimal GetUnitaryTaxedAmount()
        {
            return Price + GetUnitaryTax();
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}