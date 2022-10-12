namespace TellDontAskKata.Main.Domain
{
    public class Category
    {
        public Category()
        {
            Name = "";
            TaxPercentage = 0;
        }
        public Category(string name, decimal taxPercentage)
        {
            Name = name;
            TaxPercentage = taxPercentage;
        }

        private string Name { get; set; }
        public decimal TaxPercentage { get; set; }
    }
}
