using System;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.Service;
using TellDontAskKata.Main.UseCase.Exceptions;

namespace TellDontAskKata.Main.UseCase
{
    public class OrderShipmentUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IShipmentService _shipmentService;

        public OrderShipmentUseCase(
            IOrderRepository orderRepository,
            IShipmentService shipmentService)
        {
            _orderRepository = orderRepository;
            _shipmentService = shipmentService;
        }

        public void Run(OrderShipmentRequest request)
        {
            var order = _orderRepository.GetById(request.OrderId);

            switch (order.Status)
            {
                case OrderStatus.Created:
                case OrderStatus.Rejected:
                    throw new OrderCannotBeShippedException();
                case OrderStatus.Shipped:
                    throw new OrderCannotBeShippedTwiceException();
                case OrderStatus.Approved:
                default:
                    _shipmentService.Ship(order);

                    order.Status = OrderStatus.Shipped;
                    _orderRepository.Save(order);
                    break;
            }
        }
    }
}
