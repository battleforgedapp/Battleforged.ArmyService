using Battleforged.ArmyService.Application.Armies.Queries;
using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Shared;
using FastEndpoints;
using MediatR;

namespace Battleforged.ArmyService.Endpoints.Armies.FetchArmiesPaged;

public sealed class FetchArmiesPaged : EndpointWithMapping<
    FetchArmiesPagedRequest, 
    IPagedResult<Guid, FetchArmiesPagedResponse>, 
    IPagedResult<Guid, Army>
> {
    
    // ReSharper disable once MemberCanBePrivate.Global
    public IMediator Mediator { get; set; } = null!;
    
    public override void Configure() {
        Get("armies");
        AllowAnonymous();
    }

    public override async Task HandleAsync(FetchArmiesPagedRequest req, CancellationToken ct) {
        var query = new FetchArmiesPagedQuery(req.Cursor, req.Limit);
        var results = await Mediator.Send(query, ct);

        var response = MapFromEntity(results);
        await SendOkAsync(response, ct);
    }

    public override IPagedResult<Guid, FetchArmiesPagedResponse> MapFromEntity(IPagedResult<Guid, Army> e) {
        return new PagedResult<Guid, FetchArmiesPagedResponse>(
            e.Results.Select(x => new FetchArmiesPagedResponse {
                Id = x.Id,
                ParentArmyId = x.ParentArmyId,
                Name = x.Name
            }),
            e.TotalCount,
            e.PageSize,
            e.Next,
            e.Previous
        );
    }
}