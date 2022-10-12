using System;

namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public OrderItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
            TaxedAmount = product.GetUnitaryTaxedAmount() * quantity;
            Tax = product.GetUnitaryTax() * quantity;
        }

        public string GetProductName()
        {
            return Product.GetName();
        }

        public decimal GetProductPrice()
        {
            return Product.GetPrice();
        }

        public Product GetProduct()
        {
            return Product;
        }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TaxedAmount { get; set; }
        public decimal Tax { get; set; }
    }
}
