using FastEndpoints;
using FluentValidation;

namespace Battleforged.ArmyService.Endpoints.Armies.FetchArmiesPaged;

public sealed class FetchArmiesPagedValidator : Validator<FetchArmiesPagedRequest> {

    public FetchArmiesPagedValidator() {
        RuleFor(x => x.Limit)
            .InclusiveBetween(10, 100)
            .WithMessage("Page size cannot be more than 100 entries and no less than 10.");
    }
}