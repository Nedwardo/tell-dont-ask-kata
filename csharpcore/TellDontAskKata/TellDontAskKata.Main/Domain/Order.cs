using System;
using System.Collections.Generic;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Main.UseCase.Exceptions;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public void PopulateOrder(SellItemsRequest request, IProductCatalog _productCatalog)
        {
            foreach (var itemRequest in request.Requests)
            {
                var product = _productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                    throw new UnknownProductException();

                var orderItem = new OrderItem
                {
                    Product = product,
                    Quantity = itemRequest.Quantity,
                    Tax = product.GetUnitaryTax() * itemRequest.Quantity,
                    TaxedAmount = product.GetUnitaryTaxedAmount() * itemRequest.Quantity
                };
                AddOrderItem(orderItem);
            }
        }

        private void AddOrderItem(OrderItem orderItem)
        {
            Items.Add(orderItem);
            Total += orderItem.TaxedAmount;
            Tax += orderItem.Tax;
        }
        public void UpdateStatus(OrderApprovalRequest orderApprovalRequest)
        {
            Status = Status switch
            {
                OrderStatus.Shipped => 
                    throw new ShippedOrdersCannotBeChangedException(),
                OrderStatus.Approved when !orderApprovalRequest.Approved =>
                    throw new ApprovedOrderCannotBeRejectedException(),
                OrderStatus.Rejected when Status == OrderStatus.Rejected =>
                    throw new RejectedOrderCannotBeApprovedException(),
                _ => orderApprovalRequest.Approved ? OrderStatus.Approved : OrderStatus.Rejected
            };
        }
        public decimal Total { get; set; }
        public string Currency { get; set; }
        public IList<OrderItem> Items { get; set; }
        public decimal Tax { get; set; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        public void Ship()
        {
            Status = OrderStatus.Shipped;
        }
    }
}
