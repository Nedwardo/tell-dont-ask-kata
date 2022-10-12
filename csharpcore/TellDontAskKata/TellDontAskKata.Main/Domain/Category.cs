namespace TellDontAskKata.Main.Domain
{
    public class Category
    {
        public Category(string name, decimal taxPercentage)
        {
            _name = name;
            TaxPercentage = taxPercentage;
        }

        private string _name;
        public decimal TaxPercentage { get; }
    }
}
