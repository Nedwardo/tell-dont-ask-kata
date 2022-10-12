using System.Collections.Generic;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Main.UseCase.Exceptions;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public Order(decimal total, string currency, IList<OrderItem> items, OrderStatus status, int id)
        {
            _total = total;
            _currency = currency;
            _items = items;
            _status = status;
            _id = id;
        }
        public void PopulateOrder(SellItemsRequest request, IProductCatalog productCatalog)
        {
            foreach (var itemRequest in request.Requests)
            {
                var product = productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                    throw new UnknownProductException();

                var orderItem = new OrderItem(product, itemRequest.Quantity);
                AddOrderItem(orderItem);
            }
        }
        private void AddOrderItem(OrderItem orderItem)
        {
            _items.Add(orderItem);
            _total += orderItem.TaxedAmount;
            _tax += orderItem.Tax;
        }
        public void UpdateStatus(OrderApprovalRequest orderApprovalRequest)
        {
            _status = _status switch
            {
                OrderStatus.Shipped => 
                    throw new ShippedOrdersCannotBeChangedException(),
                OrderStatus.Approved when !orderApprovalRequest.Approved =>
                    throw new ApprovedOrderCannotBeRejectedException(),
                OrderStatus.Rejected when _status == OrderStatus.Rejected =>
                    throw new RejectedOrderCannotBeApprovedException(),
                _ => orderApprovalRequest.Approved ? OrderStatus.Approved : OrderStatus.Rejected
            };
        }

        public decimal GetTotal()
        {
            return _total;
        }

        public string GetCurrency()
        {
            return _currency;
        }
        public int ItemsCount()
        {
            return _items.Count;
        }
        public OrderItem GetItem(int index)
        {
            return _items[index];
        }

        public decimal GetTax()
        {
            return _tax;
        }

        public bool IsId(int id)
        {
            return _id == id;
        }

        public void Ship()
        {
            switch (_status)
            {
                case OrderStatus.Created:
                case OrderStatus.Rejected:
                    throw new OrderCannotBeShippedException();
                case OrderStatus.Shipped:
                    throw new OrderCannotBeShippedTwiceException();
                case OrderStatus.Approved:
                default:
                    _status = OrderStatus.Shipped;
                    return;
            }
        }

        public OrderStatus GetStatus()
        {
            return _status;
        }

        private decimal _total;
        private readonly string _currency;
        private readonly IList<OrderItem> _items;
        private decimal _tax;
        private OrderStatus _status;
        private readonly int _id;
    }
}
