﻿using Domain.Common.Abstractions;
using Domain.Common.Errors;
using Domain.Common.Result;
using Domain.Core.Queue;
using Domain.Core.User;

namespace Domain.Core.Subscription;

public sealed class QueueSubscriptionEntity : SubscriptionEntity
{
    private readonly List<QueueEntity> _subscribedQueues;

    public QueueSubscriptionEntity(
        Guid id,
        UserEntity user,
        DateOnly creationDateUtc,
        SpbDateTime? modifiedOn = null)
        : base(id, user, creationDateUtc, modifiedOn)
    {
        _subscribedQueues = new List<QueueEntity>();
    }

    public IReadOnlyCollection<QueueEntity> SubscribedQueues => _subscribedQueues;

    /// <summary>
    /// Subscribes queue to the newsletter.
    /// </summary>
    /// <param name="queue">queue to be subscribed.</param>
    /// <returns>subscribed queue entity.</returns>
    /// <remarks>returns failure result, when queue is already subscribed.</remarks>
    public Result<QueueEntity> Subscribe(QueueEntity queue)
    {
        if (_subscribedQueues.Contains(queue))
        {
            return new Result<QueueEntity>(DomainErrors.Subscription.ContainsQueueWithId(queue.Id));
        }

        _subscribedQueues.Add(queue);

        return queue;
    }

    /// <summary>
    /// Unsubscribes queue from the newsletter.
    /// </summary>
    /// <param name="queue">queue to be unsubscribed.</param>
    /// <returns>unsubscribed queue.</returns>
    /// <remarks>returns failure result, when queue is not subscribed.</remarks>
    public Result<QueueEntity> Unsubscribe(QueueEntity queue)
    {
        if (_subscribedQueues.Contains(queue) is false)
        {
            return new Result<QueueEntity>(DomainErrors.Subscription.QueueIsNotInSubscription(queue.Id));
        }

        _subscribedQueues.Remove(queue);

        return queue;
    }
}