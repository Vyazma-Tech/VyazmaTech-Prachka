﻿using Domain.Common.Abstractions;

namespace Domain.Core.Queue.Events;

/// <summary>
/// Week ended. New queues should be seeded for next week.
/// Subscriptions should be reset.
/// </summary>
public sealed class WeekEndedDomainEvent : IDomainEvent
{
    public WeekEndedDomainEvent(QueueEntity queue)
    {
        Queue = queue;
    }

    public QueueEntity Queue { get; }
}