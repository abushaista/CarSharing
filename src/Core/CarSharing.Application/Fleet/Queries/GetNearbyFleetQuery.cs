using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;

namespace CarSharing.Application.Fleet.Queries;

public sealed record GetNearbyFleetQuery(int Lat, int Lon) : IQuery<List<Car>>;