using FastEndpoints;
using Mediator;
using VyazmaTech.Prachka.Application.Contracts.Identity.Queries;
using VyazmaTech.Prachka.Application.Dto.Identity;
using VyazmaTech.Prachka.Presentation.Authorization;

namespace VyazmaTech.Prachka.Presentation.Endpoints.Identity.V1;

internal class GetAllRolesEndpoint : EndpointWithoutRequest<AllRolesDto>
{
    private const string FeatureName = "GetAllRoles";
    private readonly ISender _sender;

    public GetAllRolesEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Get("roles");
        Policies($"{AuthorizeFeatureAttribute.Prefix}{FeatureScope.Name}:{FeatureName}");
        Group<IdentityEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var query = default(GetAllRoles.Query);

        GetAllRoles.Response response = await _sender.Send(query, ct);

        await SendOkAsync(response.Roles, ct);
    }
}