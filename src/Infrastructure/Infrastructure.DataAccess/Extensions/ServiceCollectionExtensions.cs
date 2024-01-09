﻿using Application.DataAccess.Contracts;
using Domain.Core.Order;
using Domain.Core.Queue;
using Domain.Core.Subscription;
using Domain.Core.User;
using Domain.Kernel;
using Infrastructure.DataAccess.Contexts;
using Infrastructure.DataAccess.Contracts;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        Action<DbContextOptionsBuilder> options)
    {
        services.AddDbContext<DatabaseContext>(options);
        services.AddScoped<IUnitOfWork, DatabaseContext>();

        services.AddScoped<IUserRepository, UserRepositoryBase>();
        services.AddScoped<IQueueRepository, QueueRepositoryBase>();
        services.AddScoped<IOrderRepository, OrderRepositoryBase>();
        services.AddScoped<IOrderSubscriptionRepository, OrderSubscriptionRepositoryBase>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }

    public static async Task UseDatabase(this IServiceScope scope)
    {
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await context.Database.MigrateAsync();
    }
}