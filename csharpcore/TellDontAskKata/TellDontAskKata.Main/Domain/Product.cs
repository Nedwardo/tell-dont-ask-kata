namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public Product()
        {
            _name = "";
            Price = 0;
            _category = null;
        }
        public Product(string name, decimal price, Category category)
        {
            _name = name;
            Price = price;
            _category = category;
        }
        public decimal GetUnitaryTax()
        {
            return Round((Price / 100m) * _category.GetTaxPercent());
        }
        public decimal GetUnitaryTaxedAmount()
        {
            return Round(Price + GetUnitaryTax());
        }

        public string GetName()
        {
            return _name;
        }

        private readonly string _name;
        public decimal Price { get; }
        private readonly Category _category;
        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }
}