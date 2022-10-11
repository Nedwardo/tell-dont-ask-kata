﻿using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase.Exceptions;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderCreationUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;

        public OrderCreationUseCase(
            IOrderRepository orderRepository,
            IProductCatalog productCatalog)
        {
            _orderRepository = orderRepository;
            _productCatalog = productCatalog;
        }

        public void Run(SellItemsRequest request)
        {
            var order = new Order
            {
                Status = OrderStatus.Created,
                Items = new List<OrderItem>(),
                Currency = "EUR",
                Total = 0m,
                Tax = 0m
            };

            foreach (var itemRequest in request.Requests)
            {
                var product = _productCatalog.GetByName(itemRequest.ProductName);

                if (product == null)
                    throw new UnknownProductException();

                var taxedAmount = Round(Round(product.GetUnitaryTaxedAmount()) * itemRequest.Quantity);
                var taxAmount = Round(Round(product.GetUnitaryTax()) * itemRequest.Quantity);

                var orderItem = new OrderItem
                {
                    Product = product,
                    Quantity = itemRequest.Quantity,
                    Tax = taxAmount,
                    TaxedAmount = taxedAmount
                };
                order.Items.Add(orderItem);
                order.Total += taxedAmount;
                order.Tax += taxAmount;

            }

            _orderRepository.Save(order);
        }



        public static decimal Round(decimal amount)
        {
            return decimal.Round(amount, 2, System.MidpointRounding.ToPositiveInfinity);
        }
    }
}
