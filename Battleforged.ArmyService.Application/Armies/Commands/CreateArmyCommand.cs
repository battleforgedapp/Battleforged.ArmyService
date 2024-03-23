using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Events;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace Battleforged.ArmyService.Application.Armies.Commands;

public sealed class CreateArmyCommand(Guid? parentArmyId, string name) : IRequest<Guid> {

    public Guid? ParentArmyId { get; } = parentArmyId;

    public string Name { get; } = name;
}

internal sealed class CreateArmyCommandHandler(
    IArmyRepository armyRepository,
    IEventOutboxRepository eventOutboxRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateArmyCommand, Guid> {
    
    public async Task<Guid> Handle(CreateArmyCommand request, CancellationToken cancellationToken) {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try {
            var entity = new Army {
                ParentArmyId = request.ParentArmyId,
                Name = request.Name
            };

            // add the new entity into the db
            await armyRepository.AddAsync(entity, cancellationToken);
            
            // add a new event to our outbox
            var evt = new ArmyCreatedEvent {
                ArmyId = entity.Id,
                ParentArmyId = entity.ParentArmyId,
                Name = entity.Name,
                CreatedDate = entity.CreatedDate
            };

            await eventOutboxRepository.AddAsync(new EventOutbox {
                EventName = ArmyCreatedEvent.ArmyCreatedEventRoute,
                EventData = JsonConvert.SerializeObject(evt)
            }, cancellationToken);
            
            // commit the transaction and return the new army id
            await unitOfWork.CommitAsync(cancellationToken);
            return entity.Id;
        }
        catch {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}