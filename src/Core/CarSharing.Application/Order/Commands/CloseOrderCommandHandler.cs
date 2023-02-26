using System;
using CarSharing.Application.Abstractions;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Order.Common;
using CarSharing.Domain.Order;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Order.Commands
{
    public class CloseOrderCommandHandler : ICommandHandler<CloseOrderCommand, OrderResult>
    {
        private readonly ICarRepository _carRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBillCalculator _billCalculator;
        private readonly IBillService _billService;
        public CloseOrderCommandHandler(ICarRepository carRepository,
            IBookingRepository bookingRepository,
            IUserRepository userRepository,
            IBillCalculator billCalculator,
            IBillService billService)
        {
            _carRepository = carRepository;
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _billCalculator = billCalculator;
            _billService = billService;
        }

        public async Task<Result<OrderResult>> Handle(CloseOrderCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarByLicenseNo(request.LicenseNumber);
            if(car is null)
            {
                return Result.Failure<OrderResult>(new Error("404", $"No car identified with License number {request.LicenseNumber} in our database"));
            }
            if (car.Available)
            {
                return Result.Failure<OrderResult>(new Error("404", $"Car with License number {request.LicenseNumber} is not in booked status"));
            }
            var order = await _bookingRepository.GetBookingByCarId(car.Id);
            if(order is null)
            {
                return Result.Failure<OrderResult>(new Error("404", $"Car with License number {request.LicenseNumber} is not in booked status"));
            }
            var user = await _userRepository.GetUserByEmail(request.Email);
            if(order.UserId != user.Id)
            {
                return Result.Failure<OrderResult>(new Error("404", $"Car with License number {request.LicenseNumber} is book by another customer"));
            }

            car.Available = false;
            await _carRepository.Update(car);
            order.EndDate = DateTime.UtcNow;
            await _bookingRepository.Update(order);
            var billTotal = _billCalculator.Calculate(order,car);
            await _billService.GenerateBill(order, user.Id, billTotal.Total);
            
            return billTotal;
        }

        
    }
}

