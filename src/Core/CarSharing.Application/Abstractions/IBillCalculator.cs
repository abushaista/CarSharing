using System;
using CarSharing.Application.Order.Common;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Abstractions;

public interface IBillCalculator
{
	OrderResult Calculate(Booking order, Car car);
}

