using System;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Abstractions;

public interface IBillService
{
    Task<bool> GenerateBill(Booking Order, Guid UserId, float Total);
}

