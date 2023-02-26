using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Order;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;
using MediatR;

namespace CarSharing.Application.Order.Commands
{
    public class CreateBookingCommandHandler : ICommandHandler<CreateBookingCommand, Booking>
    {
        private readonly ICarRepository _carRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;

        public CreateBookingCommandHandler(ICarRepository carRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<Booking>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var booked = await BookingCar(request.Lat, request.Lon);
            if(booked is Car)
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                var order = new Booking();
                order.CarId = ((Car)booked).Id;
                order.Car = booked;
                order.UserId = user.Id;
                await _bookingRepository.Add(order);
                return order;
            }

            return (Result<Booking>)Result.Failure(new Error("404", "No fleet available"));
            
        }

        private async Task<Car?> BookingCar(int Lat, int Lon)
        {
            var data = await _carRepository.GetAllActiveCarWithinRange(Lat, Lon);
            foreach(var car in data)
            {
                car.Available = false;
                if(await _carRepository.Update(car))
                {
                    return car;
                }
            }
            return null;
        }
    }
}

