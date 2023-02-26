using System;
namespace CarSharing.Contracts.Order;

public sealed record OrderResponse(string LicenseNumber, Guid OrderId, float Total);

