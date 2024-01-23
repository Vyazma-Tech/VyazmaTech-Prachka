﻿using Domain.Core.Order;
using Infrastructure.DataAccess.Models;

namespace Infrastructure.DataAccess.Mapping;

public static class OrderMapping
{
    public static OrderEntity MapTo(OrderModel model)
    {
        return new OrderEntity(
            model.Id,
            model.QueueId,
            model.UserId,
            Enum.Parse<OrderStatus>(model.Status),
            model.CreationDate,
            model.ModifiedOn);
    }

    public static OrderModel MapFrom(OrderEntity entity)
    {
        return new OrderModel
        {
            Id = entity.Id,
            QueueId = entity.Queue,
            UserId = entity.User,
            CreationDate = entity.CreationDateTime,
            ModifiedOn = entity.ModifiedOn,
            Status = entity.Status.ToString()
        };
    }
}