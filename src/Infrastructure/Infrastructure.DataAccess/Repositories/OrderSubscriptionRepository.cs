﻿using Application.DataAccess.Contracts.Querying.OrderSubscription;
using Application.DataAccess.Contracts.Repositories;
using Domain.Core.Subscription;
using Infrastructure.DataAccess.Contexts;
using Infrastructure.DataAccess.Mapping;
using Infrastructure.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess.Repositories;

internal class OrderSubscriptionRepository : RepositoryBase<OrderSubscriptionEntity, OrderSubscriptionModel>,
    IOrderSubscriptionRepository
{
    public OrderSubscriptionRepository(DatabaseContext context)
        : base(context)
    {
    }

    public IAsyncEnumerable<OrderSubscriptionEntity> QueryAsync(
        OrderSubscriptionQuery specification,
        CancellationToken cancellationToken)
    {
        IQueryable<OrderSubscriptionModel> queryable = DbSet;

        queryable = queryable
            .Include(x => x.User)
            .ThenInclude(x => x.Orders);

        if (specification.Id is not null)
        {
            queryable = queryable.Where(x => x.Id == specification.Id);
        }

        if (specification.OrderId is not null)
        {
            queryable = queryable.Where(x => x.Orders.Any(order => order.Id == specification.OrderId));
        }

        if (specification.UserId is not null)
        {
            queryable = queryable.Where(x => x.UserId == specification.UserId);
        }

        if (specification.Limit is not null)
        {
            if (specification.Page is not null)
            {
                queryable = queryable.Skip(specification.Page.Value * specification.Limit.Value)
                    .Take(specification.Limit.Value);
            }
            else
            {
                queryable = queryable.Take(specification.Limit.Value);
            }
        }

        var finalQueryable = queryable.Select(subscription => new
        {
            subscription,
            orders = subscription.Orders.Select(x => x.Id)
        });

        return finalQueryable.AsAsyncEnumerable().Select(x => MapTo(x.subscription, x.orders));
    }

    protected override OrderSubscriptionModel MapFrom(OrderSubscriptionEntity entity)
    {
        return OrderSubscriptionMapping.MapFrom(entity);
    }

    protected override bool Equal(OrderSubscriptionEntity entity, OrderSubscriptionModel model)
    {
        return entity.Id.Equals(model.Id);
    }

    protected override void UpdateModel(OrderSubscriptionModel model, OrderSubscriptionEntity entity)
    {
        model.ModifiedOn = entity.ModifiedOn;
    }

    private static OrderSubscriptionEntity MapTo(OrderSubscriptionModel model, IEnumerable<Guid> orderIds)
    {
        return OrderSubscriptionMapping.MapTo(model, orderIds.ToHashSet());
    }
}