using Battleforged.ArmyService.Application.Tools;
using FastEndpoints;
using MediatR;

namespace Battleforged.ArmyService.Endpoints.ImportData; 

public class ImportData : Endpoint<EmptyRequest, EmptyResponse> {
    
    // TODO: we want to add roles to only allow admins to upload these spreadsheets
    
    // ReSharper disable once MemberCanBePrivate.Global
    public IMediator Mediator { get; set; } = null!;
    
    public override void Configure() {
        Post("/api/tools/import");
        AllowFileUploads();
        
        AllowAnonymous(); // TEMP TODO
    }

    public override async Task HandleAsync(EmptyRequest req, CancellationToken ct) {
        if (!Files.Any()) {
            AddError("No file was supplied.");
            ThrowIfAnyErrors();
        }

        var file = Files[0];
        await using var fileStream = file.OpenReadStream();

        var command = new ImportSpreadsheetDataCommand(fileStream);
        await Mediator.Send(command, ct);
        await SendOkAsync(ct);
    }
}