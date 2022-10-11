using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Product
    {
        public decimal GetUnitaryTax()
        {
            return OrderCreationUseCase.Round((Price / 100m) * Category.TaxPercentage);
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
    }
}