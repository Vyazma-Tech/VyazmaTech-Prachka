﻿using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace VyazmaTech.Prachka.Presentation.Endpoints.Extensions;

public static class EndpointExtensions
{
    public static Task SendPartialContentAsync<TResponse>(
        this IEndpoint endpoint,
        TResponse response,
        CancellationToken cancellationToken)
    {
        return endpoint.HttpContext.Response.SendAsync(
            response,
            StatusCodes.Status206PartialContent,
            cancellation: cancellationToken);
    }

    public static Task SendProblemsAsync(
        this IEndpoint endpoint,
        IResult result)
    {
        return endpoint.HttpContext.Response.SendResultAsync(result);
    }

    public static Task SendAcceptedAsync<TResponse>(
        this IEndpoint endpoint,
        TResponse response,
        CancellationToken cancellationToken)
    {
        return endpoint.HttpContext.Response.SendAsync(
            response,
            StatusCodes.Status202Accepted,
            cancellation: cancellationToken);
    }
}