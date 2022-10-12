using System;

namespace TellDontAskKata.Main.Domain
{
    public class OrderItem
    {
        public OrderItem()
        {
        }

        public OrderItem(Product product, int quantity, decimal taxedAmount, decimal tax)
        {
            Product = product;
            Quantity = quantity;
            TaxedAmount = taxedAmount;
            Tax = tax;
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
