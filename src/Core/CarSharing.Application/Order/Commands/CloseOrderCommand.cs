using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Order.Common;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Order.Commands;

public sealed record CloseOrderCommand(string LicenseNumber, string Email) : ICommand<OrderResult>;
	


