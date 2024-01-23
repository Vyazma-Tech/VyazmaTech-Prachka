﻿using Domain.Common.Abstractions;
using Domain.Common.Errors;
using Domain.Common.Result;
using Domain.Core.Order;

namespace Domain.Core.Subscription;

public sealed class OrderSubscriptionEntity : SubscriptionEntity
{
    private readonly HashSet<Guid> _orderIds;

    public OrderSubscriptionEntity(
        Guid id,
        Guid user,
        DateOnly creationDateUtc,
        HashSet<Guid> orderIds,
        SpbDateTime? modifiedOn = null)
        : base(id, user, creationDateUtc, modifiedOn)
    {
        _orderIds = orderIds;
    }

    public IReadOnlyCollection<Guid> SubscribedOrders => _orderIds;

    public Result<OrderEntity> Subscribe(OrderEntity order)
    {
        if (_orderIds.Contains(order.Id))
        {
            return new Result<OrderEntity>(DomainErrors.Subscription.ContainsOrderWithId(order.Id));
        }

        _orderIds.Add(order.Id);

        return order;
    }

    public Result<OrderEntity> Unsubscribe(OrderEntity order)
    {
        if (_orderIds.Contains(order.Id) is false)
        {
            return new Result<OrderEntity>(DomainErrors.Subscription.OrderIsNotInSubscription(order.Id));
        }

        _orderIds.Remove(order.Id);

        return order;
    }
}