using Battleforged.ArmyService.Domain.Abstractions;
using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;

namespace Battleforged.ArmyService.Application.Armies.Queries;

public sealed class FetchArmiesPagedQuery(Guid? cursor, int pageSize) : IRequest<IPagedResult<Guid, Army>> {

    public Guid? Cursor { get; } = cursor;

    public int PageSize { get; } = pageSize;
}

internal sealed class FetchArmiesPagedQueryHandler(
    IArmyRepository armyRepository    
) : IRequestHandler<FetchArmiesPagedQuery, IPagedResult<Guid, Army>> {

    public async Task<IPagedResult<Guid, Army>> Handle(FetchArmiesPagedQuery request, CancellationToken cancellationToken)
        => await armyRepository.FetchPagedAsync(request.Cursor, request.PageSize, cancellationToken);
}