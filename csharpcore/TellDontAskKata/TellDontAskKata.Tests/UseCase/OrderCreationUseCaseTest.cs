﻿using System;
using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Tests.Doubles;
using Xunit;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderCreationUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly IProductCatalog _productCatalog;
        private readonly OrderCreationUseCase _useCase;

        public OrderCreationUseCaseTest()
        {
            var food = new Category (
                "food",
                10m
            );

            _productCatalog = new InMemoryProductCatalog(new List<Product>
            {
                new Product("salad", 3.56m, food),
                new Product("tomato", 4.65m, food)
            });

            _orderRepository = new TestOrderRepository();

            _useCase = new OrderCreationUseCase(_orderRepository, _productCatalog);
        }


        [Fact]
        public void SellMultipleItems()
        {
            var saladRequest = new SellItemRequest
            {
                ProductName = "salad",
                Quantity = 2
            };

            var tomatoRequest = new SellItemRequest
            {
                ProductName = "tomato",
                Quantity = 3
            };

            var request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest> { saladRequest, tomatoRequest }
            };

            _useCase.Run(request);

            Order insertedOrder = _orderRepository.GetSavedOrder();
            Assert.Equal(OrderStatus.Created, insertedOrder.GetStatus());
            Assert.Equal(23.20m, insertedOrder.GetTotal()); 
            Assert.Equal(2.13m, insertedOrder.GetTax());
            Assert.Equal("EUR", insertedOrder.GetCurrency());
            Assert.Equal(2, insertedOrder.ItemsCount());
            Assert.Equal("salad", insertedOrder.GetItem(0).Product.GetName());
            Assert.Equal(3.56m, insertedOrder.GetItem(0).Product.GetPrice());
            Assert.Equal(2, insertedOrder.GetItem(0).Quantity);
            Assert.Equal(7.84m, insertedOrder.GetItem(0).TaxedAmount);
            Assert.Equal(0.72m, insertedOrder.GetItem(0).Tax);
            Assert.Equal("tomato", insertedOrder.GetItem(1).Product.GetName());
            Assert.Equal(4.65m, insertedOrder.GetItem(1).Product.GetPrice());
            Assert.Equal(3, insertedOrder.GetItem(1).Quantity);
            Assert.Equal(15.36m, insertedOrder.GetItem(1).TaxedAmount);
            Assert.Equal(1.41m, insertedOrder.GetItem(1).Tax);
        }

        [Fact]
        public void UnknownProduct()
        {
            var request = new SellItemsRequest
            {
                Requests = new List<SellItemRequest> { 
                    new SellItemRequest { ProductName = "unknown product"}
                }
            };

            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<UnknownProductException>(actionToTest);
        }



    }
}
