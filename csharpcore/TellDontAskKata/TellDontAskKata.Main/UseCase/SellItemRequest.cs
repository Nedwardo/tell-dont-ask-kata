using TellDontAskKata.Main.Domain;


namespace TellDontAskKata.Main.UseCase
{
    public class SellItemRequest
    {
        private static decimal getUnitaryTax(Product product)
        {
            return OrderCreationUseCase.Round((product.Price / 100m) * product.Category.TaxPercentage);
        }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
    }
}
