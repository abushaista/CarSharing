using CarSharing.Domain.Fleet;

namespace CarSharing.Application.Fleet.Common;

public sealed record CarRouteResult(Car Car, List<Coordinate> Routes);

public sealed record Coordinate(int Lat, int Lon);