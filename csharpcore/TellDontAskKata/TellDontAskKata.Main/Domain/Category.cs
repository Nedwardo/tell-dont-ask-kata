namespace TellDontAskKata.Main.Domain
{
    public class Category
    {
        public Category(string name, decimal taxPercentage)
        {
            _name = name;
            _taxPercentage = taxPercentage;
        }

        public decimal GetTaxPercent()
        {
            return _taxPercentage;
        }

        private string _name;
        private readonly decimal _taxPercentage;
    }
}
