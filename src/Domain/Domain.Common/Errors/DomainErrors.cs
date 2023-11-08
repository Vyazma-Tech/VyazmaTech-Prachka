﻿namespace Domain.Common.Errors;

public static class DomainErrors
{
    public static class Order
    {
        public static Error AlreadyPaid => new (
            $"{nameof(Order)}.{nameof(AlreadyPaid)}",
            "The order was already paid.");

        public static Error IsReady => new (
            $"{nameof(Order)}.{nameof(IsReady)}",
            "The order is ready.");

        public static Error UnableToTransferInTheSameQueue => new (
            $"{nameof(Order)}.{nameof(UnableToTransferInTheSameQueue)}",
            "The order cannot be transferred in the same queue.");
    }

    public static class QueueDate
    {
        public static Error InThePast => new (
            $"{nameof(QueueDate)}.{nameof(InThePast)}",
            "The queue date should be later than now");

        public static Error NotNextWeek => new (
            $"{nameof(QueueDate)}.{nameof(NotNextWeek)}",
            "The queue date be on this week");
    }

    public static class Queue
    {
        public static Error ContainsOrderWithId(Guid id) => new (
            $"{nameof(Queue)}.{nameof(ContainsOrderWithId)}",
            $"The queue already contains order with id: {id}");

        public static Error OrderIsNotInQueue(Guid id) => new (
            $"{nameof(Queue)}.{nameof(OrderIsNotInQueue)}",
            $"The queue does not contain order with id: {id}");

        public static Error InvalidNewCapacity => new (
            $"{nameof(Queue)}.{nameof(InvalidNewCapacity)}",
            "New queue capacity should not be less then current capacity.");

        public static Error Overfull => new (
            $"{nameof(Queue)}.{nameof(Overfull)}",
            "Queue is overfull. You are not able to enter it now.");
    }

    public static class Subscription
    {
        public static Error ContainsOrderWithId(Guid id) => new (
            $"{nameof(Subscription)}.{nameof(ContainsOrderWithId)}",
            $"The subscription already contains order with id: {id}");

        public static Error OrderIsNotInSubscription(Guid id) => new (
            $"{nameof(Subscription)}.{nameof(OrderIsNotInSubscription)}",
            $"The subscription does not contain order with id: {id}");
    }

    public static class TelegramId
    {
        public static Error NullOrEmpty => new (
            $"{nameof(TelegramId)}.{nameof(NullOrEmpty)}",
            "Telegram ID should not be null or empty.");

        public static Error InvalidFormat => new (
            $"{nameof(TelegramId)}.{nameof(InvalidFormat)}",
            "Telegram ID should be a number.");
    }

    public static class Fullname
    {
        public static Error NameIsNullOrEmpty => new (
            $"{nameof(Fullname)}.{nameof(NameIsNullOrEmpty)}",
            "Name should not be null or empty.");

        public static Error InvalidNameFormat => new (
            $"{nameof(Fullname)}.{nameof(InvalidNameFormat)}",
            "Name should start with uppercase and contain only letters");
    }

    public static class Capacity
    {
        public static Error Negative => new (
            $"{nameof(Capacity)}.{nameof(Negative)}",
            "Capacity should be at least zero.");
    }

    public static class QueueActivityBoundaries
    {
        public static Error EmptyRange => new (
            $"{nameof(QueueActivityBoundaries)}.{nameof(EmptyRange)}",
            "The queue activity boundaries should describe time range during the day.");
    }
}