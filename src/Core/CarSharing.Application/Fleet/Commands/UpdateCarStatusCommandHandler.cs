using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Fleet.Commands
{
    public sealed class UpdateCarStatusCommandHandler : ICommandHandler<UpdateCarStatusCommand>
    {
        private readonly ICarRepository _carRepository;

        public UpdateCarStatusCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Result> Handle(UpdateCarStatusCommand request, CancellationToken cancellationToken)
        {
            if(await _carRepository.GetCarById(request.id) is Car car)
            {
                car.IsActive = request.status;
                await _carRepository.Update(car);
                return Result.Success();
            }
            return Result.Failure(new Error("404", "Car not found"));
        }
    }
}

