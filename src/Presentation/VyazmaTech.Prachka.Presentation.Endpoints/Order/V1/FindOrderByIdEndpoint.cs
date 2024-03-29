﻿using FastEndpoints;
using Mediator;
using VyazmaTech.Prachka.Application.Contracts.Orders.Queries;
using VyazmaTech.Prachka.Application.Dto.Order;
using VyazmaTech.Prachka.Domain.Common.Result;
using VyazmaTech.Prachka.Presentation.Endpoints.Extensions;
using VyazmaTech.Prachka.Presentation.Endpoints.Order.V1.Models;

namespace VyazmaTech.Prachka.Presentation.Endpoints.Order.V1;

internal class FindOrderByIdEndpoint : Endpoint<OrderWithIdRequest, OrderDto>
{
    private readonly ISender _sender;

    public FindOrderByIdEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("{orderId}");
        AllowAnonymous();
        Group<OrderEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(OrderWithIdRequest req, CancellationToken ct)
    {
        var query = new OrderById.Query(req.OrderId);

        Result<OrderById.Response> response = await _sender.Send(query, ct);

        await response.Match(
            success => SendOkAsync(success.Order, ct),
            _ => this.SendProblemsAsync(response.ToProblemDetails()));
    }
}