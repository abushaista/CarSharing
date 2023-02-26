using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Order.Commands;


public record CreateBookingCommand(int Lat, int Lon, string Email): ICommand<Booking>;