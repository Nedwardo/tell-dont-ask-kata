namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public Product(string name, decimal price, Category category)
        {
            _name = name;
            _price = price;
            _category = category;
        }
        public decimal GetUnitaryTax()
        {
            return Round((_price / 100m) * _category.GetTaxPercent());
        }
        public decimal GetUnitaryTaxedAmount()
        {
            return Round(_price + GetUnitaryTax());
        }
        
        public bool IsName(string name)
        {
            return _name == name;
        }

        public string GetName()
        {
            return _name;
        }

        public decimal GetPrice()
        {
            return _price;
        }

        private readonly string _name;
        private readonly decimal _price;
        private readonly Category _category;
        private static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }
}