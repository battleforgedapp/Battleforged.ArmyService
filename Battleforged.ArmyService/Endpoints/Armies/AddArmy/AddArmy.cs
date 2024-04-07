using Battleforged.ArmyService.Application.Armies.Commands;
using FastEndpoints;
using MediatR;

namespace Battleforged.ArmyService.Endpoints.Armies.AddArmy;

public sealed class AddArmy : Endpoint<AddArmyRequest, AddArmyResponse> {
    
    // ReSharper disable once MemberCanBePrivate.Global
    public IMediator Mediator { get; set; } = null!;
    
    public override void Configure() {
        Post("armies");
        Roles("admin");
    }

    public override async Task HandleAsync(AddArmyRequest req, CancellationToken ct) {
        var command = new CreateArmyCommand(req.ParentArmyId, req.Name);
        var response = await Mediator.Send(command, ct);

        await SendOkAsync(new AddArmyResponse {
            Id = response
        }, ct);
    }
}