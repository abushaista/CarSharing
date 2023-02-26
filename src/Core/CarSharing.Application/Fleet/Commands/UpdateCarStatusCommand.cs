using CarSharing.Application.Abstractions.Messaging;

namespace CarSharing.Application.Fleet.Commands;

public sealed record UpdateCarStatusCommand(Guid id, bool status) : ICommand;