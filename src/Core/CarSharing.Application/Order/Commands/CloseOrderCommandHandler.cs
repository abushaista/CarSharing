using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Order;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Order.Commands
{
    public class CloseOrderCommandHandler : ICommandHandler<CloseOrderCommand, Booking>
    {
        private readonly ICarRepository _carRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public CloseOrderCommandHandler(ICarRepository carRepository, IBookingRepository bookingRepository, IUserRepository userRepository)
        {
            _carRepository = carRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<Booking>> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarByLicenseNo(request.LicenseNumber);
            if(car is null)
            {
                return Result.Failure<Booking>(new Error("404", $"No car identified with License number {request.LicenseNumber} in our database"));
            }
            if (car.Available)
            {
                return Result.Failure<Booking>(new Error("404", $"Car with License number {request.LicenseNumber} is not in booked status"));
            }
            var order = await _bookingRepository.GetBookingByCarId(car.Id);
            if(order is null)
            {
                return Result.Failure<Booking>(new Error("404", $"Car with License number {request.LicenseNumber} is not in booked status"));
            }
            if(order.UserId != request.UserId)
            {
                return Result.Failure<Booking>(new Error("404", $"Car with License number {request.LicenseNumber} is book by another customer"));
            }

            car.Available = false;
            await _carRepository.Update(car);
            order.EndDate = DateTime.UtcNow;
            await _bookingRepository.Update(order);
            return order;
        }
    }
}

