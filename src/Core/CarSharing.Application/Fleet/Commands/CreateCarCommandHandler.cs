using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Fleet.Commands;

public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, Car>
{
    private readonly ICarRepository _carRepository;

    public CreateCarCommandHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Result<Car>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {

        if(await _carRepository.GetCarByLicenseNo(request.LicenseNumber) is not null)
        {
            return Result.Failure<Car>(new Error("403", "Car within the same license number already exist"));
        }

        var car = new Car { LicenseNumber = request.LicenseNumber, Lat = request.Lat, Lon = request.Lon, Seat = request.Seat };
        await _carRepository.Add(car);
        return car;
    }
}