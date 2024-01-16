﻿using System.Collections;
using Domain.Common.Result;
using Domain.Core.Order;
using Domain.Core.Queue;
using Domain.Core.User;
using Domain.Core.ValueObjects;
using Infrastructure.Tools;

namespace Test.Core.Domain.Entities.ClassData;

public sealed class OrderClassData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var dateTimeProvider = new SpbDateTimeProvider();
        UserEntity user = UserClassData.Create();
        DateTime queueDate = DateTime.UtcNow.AddDays(1);

        var queue = new QueueEntity(
            Guid.NewGuid(),
            Capacity.Create(10).Value,
            QueueDate.Create(DateOnly.FromDateTime(queueDate), dateTimeProvider).Value,
            QueueActivityBoundaries.Create(
                TimeOnly.FromDateTime(queueDate),
                TimeOnly.FromDateTime(queueDate).AddHours(5)).Value,
            QueueState.Active);

        Result<OrderEntity> order = OrderEntity.Create(
            Guid.NewGuid(),
            user,
            queue,
            OrderStatus.New,
            dateTimeProvider.SpbDateTimeNow);
        
        order.Value.ClearDomainEvents();

        yield return new object[] { order.Value };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}