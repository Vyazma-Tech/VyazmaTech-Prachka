﻿using Application.DataAccess.Contracts.Repositories;
using Domain.Common.Errors;
using Domain.Common.Result;
using Domain.Core.Order;
using Domain.Core.Queue;
using Domain.Kernel;

namespace Application.Core.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IDateTimeProvider _timeProvider;

    public OrderService(
        IOrderRepository orderRepository,
        IDateTimeProvider timeProvider)
    {
        _orderRepository = orderRepository;
        _timeProvider = timeProvider;
    }

    public Result<OrderEntity> ProlongOrder(
        OrderEntity order,
        QueueEntity previousQueue,
        QueueEntity queue)
    {
        if (order.Queue.Equals(queue.Id))
        {
            return new Result<OrderEntity>(DomainErrors.Order.UnableToTransferIntoSameQueue);
        }

        if (queue.Capacity.Equals(queue.Orders.Count))
        {
            return new Result<OrderEntity>(DomainErrors.Order.UnableToTransferIntoFullQueue);
        }

        Result<OrderEntity> removalResult = previousQueue.Remove(order);
        if (removalResult.IsFaulted)
        {
            return removalResult;
        }

        Result<OrderEntity> entranceResult = queue.Add(order, _timeProvider.SpbDateTimeNow);
        if (entranceResult.IsFaulted)
        {
            return entranceResult;
        }

        order.Prolong(queue, _timeProvider.SpbDateTimeNow);
        _orderRepository.Update(order);

        return order;
    }
}