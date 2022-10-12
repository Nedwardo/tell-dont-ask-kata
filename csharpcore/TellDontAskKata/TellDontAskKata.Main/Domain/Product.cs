namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public Product()
        {
            Name = "";
            Price = 0;
            Category = null;
        }
        public Product(string name, decimal price, Category category)
        {
            Name = name;
            Price = price;
            Category = category;
        }
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