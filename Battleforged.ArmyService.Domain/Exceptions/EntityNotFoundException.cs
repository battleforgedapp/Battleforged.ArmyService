namespace Battleforged.ArmyService.Domain.Exceptions;

public class EntityNotFoundException<T, TU>(T id)
    : Exception($"Could not find entity of type '{nameof(TU)}' with ID: '{id}'")
    where T : struct
    where TU : class;