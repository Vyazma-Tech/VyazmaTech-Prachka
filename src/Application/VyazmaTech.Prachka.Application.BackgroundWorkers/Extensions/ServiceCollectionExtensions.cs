﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VyazmaTech.Prachka.Application.BackgroundWorkers.Configuration;
using VyazmaTech.Prachka.Application.BackgroundWorkers.Integration;
using VyazmaTech.Prachka.Application.BackgroundWorkers.Queue;

namespace VyazmaTech.Prachka.Application.BackgroundWorkers.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWorkersConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<QueueWorkerConfiguration>(configuration.GetSection(QueueWorkerConfiguration.SectionKey));

        return services;
    }

    public static IServiceCollection AddWorkers(this IServiceCollection services)
    {
        services
            .AddHostedService<QueueActivatorBackgroundWorker>()
            .AddHostedService<QueueActivityBackgroundWorker>()
            .AddHostedService<QueueAvailablePositionBackgroundWorker>()
            .AddHostedService<OutboxMessagesProcessingBackgroundWorker>();

        return services;
    }
}