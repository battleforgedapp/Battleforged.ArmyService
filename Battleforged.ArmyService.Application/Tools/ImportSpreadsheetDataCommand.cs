using Battleforged.ArmyService.Domain.Abstractions;
using MediatR;

namespace Battleforged.ArmyService.Application.Tools; 

public record ImportSpreadsheetDataCommand(Stream FileStream) : IRequest<bool>;

internal sealed class ImportSpreadsheetDataCommandHandler(ISpreadsheetImporter importer)
    : IRequestHandler<ImportSpreadsheetDataCommand, bool> {

    public async Task<bool> Handle(ImportSpreadsheetDataCommand request, CancellationToken cancellationToken)
        => await importer.ImportServiceDataFromSpreadsheetAsync(request.FileStream, cancellationToken);
}