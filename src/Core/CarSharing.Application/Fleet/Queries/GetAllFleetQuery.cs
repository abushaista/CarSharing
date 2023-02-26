using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;

namespace CarSharing.Application.Fleet.Queries;

public sealed record GetAllFleetQuery(string? LicenseNumber, int? Seat, float? MinPrice, float? MaxPrice,int Page, int Rows) : IQuery<List<Car>>;

