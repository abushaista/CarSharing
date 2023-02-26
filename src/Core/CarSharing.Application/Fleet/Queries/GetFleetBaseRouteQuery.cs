using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Fleet.Common;

namespace CarSharing.Application.Fleet.Queries;

public sealed record GetFleetBaseRouteQuery(string LicenseNumber, int Lat, int Lon) : IQuery<CarRouteResult>;
	


