using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Order.Commands;

public sealed record CloseOrderCommand(string LicenseNumber, Guid UserId) : ICommand<Booking>;
	


