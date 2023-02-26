using System;
namespace CarSharing.Contracts.Fleet;

public sealed record SearchCarRequest(string? LicenseNumber, int? Seat, float? MinPrice, float? MaxPrice, int Page = 0, int Rows = 10);

