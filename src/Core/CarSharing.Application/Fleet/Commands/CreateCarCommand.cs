using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;

namespace CarSharing.Application.Fleet.Commands;

public sealed record CreateCarCommand(string LicenseNumber, int Seat, int Lat, int Lon) :ICommand<Car>;