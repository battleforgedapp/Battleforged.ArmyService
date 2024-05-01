using Battleforged.ArmyService.Domain.Entities;
using Battleforged.ArmyService.Domain.Events;
using Battleforged.ArmyService.Domain.Exceptions;
using Battleforged.ArmyService.Domain.Repositories;
using MediatR;
using Newtonsoft.Json;

namespace Battleforged.ArmyService.Application.Armies.Commands.DeleteArmy;

public sealed class DeleteArmyCommand(Guid armyId) : IRequest {

    public Guid ArmyId { get; } = armyId;
}

internal sealed class DeleteArmyCommandHandler(
    IArmyRepository armyRepository,
    IEventOutboxRepository eventOutboxRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteArmyCommand> {
    
    public async Task Handle(DeleteArmyCommand request, CancellationToken cancellationToken) {
        await unitOfWork.BeginTransactionAsync(cancellationToken);
        try {
            // find the entity we want to delete
            var entity = await armyRepository.GetByIdAsync(request.ArmyId, cancellationToken);
            if (entity is null) {
                throw new EntityNotFoundException<Guid, Army>(request.ArmyId);
            }
            
            // remove the entity, which will be a soft delete for now
            armyRepository.Delete(entity);
            
            // now create our event
            var evt = new ArmyDeletedEvent {
                ArmyId = entity.Id,
                DeletedDate = DateTime.UtcNow
            };

            await eventOutboxRepository.AddAsync(new EventOutbox {
                EventName = ArmyDeletedEvent.ArmyDeletedEventRoute,
                EventData = JsonConvert.SerializeObject(evt)
            }, cancellationToken);
            
            // commit the transaction and return
            await unitOfWork.CommitAsync(cancellationToken);
        }
        catch {
            await unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }
}