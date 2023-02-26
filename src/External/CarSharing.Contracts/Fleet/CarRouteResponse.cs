using System;
namespace CarSharing.Contracts.Fleet;

public sealed record CarRouteResponse(CarResponse Car, List<CoordinateRespons> Routes);

public sealed record CoordinateRespons(int Lat, int Lon);
	


