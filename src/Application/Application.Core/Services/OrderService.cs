﻿using Domain.Common.Errors;
using Domain.Common.Exceptions;
using Domain.Common.Result;
using Domain.Core.Order;
using Domain.Core.Queue;
using Infrastructure.DataAccess.Contracts;

namespace Application.Core.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IQueueRepository _queueRepository;

    public OrderService(IOrderRepository orderRepository, IQueueRepository queueRepository)
    {
        _orderRepository = orderRepository;
        _queueRepository = queueRepository;
    }

    public Result<OrderEntity> ProlongOrder(
        OrderEntity order,
        QueueEntity queue,
        DateTime prolongedOnUtc)
    {
        if (order.Queue.Id.Equals(queue.Id))
        {
            var exception = new DomainException(DomainErrors.Order.UnableToTransferIntoSameQueue);
            return new Result<OrderEntity>(exception);
        }

        if (queue.Capacity.Value.Equals(queue.Items.Count))
        {
            var exception = new DomainException(DomainErrors.Order.UnableToTransferIntoFullQueue);
            return new Result<OrderEntity>(exception);
        }

        Result<OrderEntity> removalResult = order.Queue.Remove(order);
        if (removalResult.IsFaulted)
        {
            return removalResult;
        }

        Result<OrderEntity> entranceResult = queue.Add(order);
        if (entranceResult.IsFaulted)
        {
            return entranceResult;
        }

        order.Prolong(queue, prolongedOnUtc);
        _orderRepository.Update(order);
        _queueRepository.Update(queue);

        return order;
    }
}