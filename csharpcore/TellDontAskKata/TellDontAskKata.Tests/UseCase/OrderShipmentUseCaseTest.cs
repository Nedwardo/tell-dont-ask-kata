﻿using System;
using System.Collections.Generic;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Main.UseCase.Exceptions;
using TellDontAskKata.Tests.Doubles;
using Xunit;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderShipmentUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly TestShipmentService _shipmentService;
        private readonly OrderShipmentUseCase _useCase;

        public OrderShipmentUseCaseTest()
        {
            _orderRepository = new TestOrderRepository();
            _shipmentService = new TestShipmentService();
            _useCase = new OrderShipmentUseCase(_orderRepository, _shipmentService);
        }


        [Fact]
        public void ShipApprovedOrder()
        {
            var initialOrder = new Order(0m, "", new List<OrderItem>(), OrderStatus.Approved, 1);
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            _useCase.Run(request);

            Assert.Equal(OrderStatus.Shipped, _orderRepository.GetSavedOrder().GetStatus());
            Assert.Same(initialOrder, _shipmentService.GetShippedOrder());
        }

        [Fact]
        public void CreatedOrdersCannotBeShipped()
        {
            var initialOrder = new Order(0m, "", new List<OrderItem>(), OrderStatus.Created, 1);
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
            Assert.Null(_shipmentService.GetShippedOrder());
        }

        [Fact]
        public void RejectedOrdersCannotBeShipped()
        {
            var initialOrder = new Order(0m, "", new List<OrderItem>(), OrderStatus.Rejected, 1);
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
            Assert.Null(_shipmentService.GetShippedOrder());
        }

        [Fact]
        public void ShippedOrdersCannotBeShippedAgain()
        {
            var initialOrder = new Order(0m, "", new List<OrderItem>(), OrderStatus.Shipped, 1);
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderShipmentRequest
            {
                OrderId = 1
            };

            Action actionToTest = () => _useCase.Run(request);

            Assert.Throws<OrderCannotBeShippedTwiceException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
            Assert.Null(_shipmentService.GetShippedOrder());
        }


    }
}
