using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Fleet.Queries;

public class GetNearbyFleetQueryHandler : IQueryHandler<GetNearbyFleetQuery, List<Car>>
{
    private readonly ICarRepository _carRepository;

    public GetNearbyFleetQueryHandler(ICarRepository carRepository)
    {
        _carRepository = carRepository;
    }

    public async Task<Result<List<Car>>> Handle(GetNearbyFleetQuery request, CancellationToken cancellationToken)
    {
        var data = await _carRepository.GetAllActiveCarWithinRange(request.Lat, request.Lat);
        return data;
    }
}