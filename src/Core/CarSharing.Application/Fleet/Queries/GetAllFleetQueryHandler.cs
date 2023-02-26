using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Fleet.Queries;

public class GetAllFleetQueryHandler : IQueryHandler<GetAllFleetQuery, List<Car>>
{
    private readonly ICarRepository _carRepository;

    public GetAllFleetQueryHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Result<List<Car>>> Handle(GetAllFleetQuery request, CancellationToken cancellationToken)
    {
        var data = await _carRepository.GetAllCars(request.LicenseNumber, request.Seat, request.MinPrice, request.MaxPrice, request.Page, request.Rows);
        return data;
    }
}

