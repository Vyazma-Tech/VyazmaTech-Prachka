﻿using FastEndpoints;
using Mediator;
using VyazmaTech.Prachka.Application.Contracts.Orders.Commands;
using VyazmaTech.Prachka.Application.Dto.Order;
using VyazmaTech.Prachka.Domain.Common.Result;
using VyazmaTech.Prachka.Presentation.Authorization;
using VyazmaTech.Prachka.Presentation.Endpoints.Extensions;
using VyazmaTech.Prachka.Presentation.Endpoints.Order.V1.Models;

namespace VyazmaTech.Prachka.Presentation.Endpoints.Order.V1;

internal class MarkAsReadyEndpoint : Endpoint<OrderWithIdRequest, OrderDto>
{
    private const string FeatureName = "MarkAsReady";
    private readonly ISender _sender;

    public MarkAsReadyEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Patch("ready");
        Policies($"{AuthorizeFeatureAttribute.Prefix}{FeatureScope.Name}:{FeatureName}");
        Group<OrderEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(OrderWithIdRequest req, CancellationToken ct)
    {
        var command = new MarkOrderAsReady.Command(req.OrderId);

        Result<MarkOrderAsReady.Response> response = await _sender.Send(command, ct);

        await response.Match(
            success => SendOkAsync(success.Order, ct),
            _ => this.SendProblemsAsync(response.ToProblemDetails()));
    }
}