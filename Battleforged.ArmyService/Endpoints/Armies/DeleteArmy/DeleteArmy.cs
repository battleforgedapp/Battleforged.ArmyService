using Battleforged.ArmyService.Application.Armies.Commands;
using FastEndpoints;
using MediatR;

namespace Battleforged.ArmyService.Endpoints.Armies.DeleteArmy;

public sealed class DeleteArmy : Endpoint<DeleteArmyRequest, EmptyResponse> {
    
    // ReSharper disable once MemberCanBePrivate.Global
    public IMediator Mediator { get; set; } = null!;
    
    public override void Configure() {
        Delete("armies/{ArmyId}");
        Roles("admin");
    }

    public override async Task HandleAsync(DeleteArmyRequest req, CancellationToken ct) {
        var command = new DeleteArmyCommand(req.ArmyId);
        await Mediator.Send(command, ct);
        await SendNoContentAsync(ct);
    }
}