using System;
using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Fleet.Common;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Fleet.Queries
{
    public class GetFleetBaseRouteQueryHandler : IQueryHandler<GetFleetBaseRouteQuery, CarRouteResult>
    {
        private readonly ICarRepository _carRepository;

        public GetFleetBaseRouteQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Result<CarRouteResult>> Handle(GetFleetBaseRouteQuery request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetCarByLicenseNo(request.LicenseNumber);
            if(car == null)
            {
                return Result.Failure<CarRouteResult>(new Error("404", $"Car within License Number {request.LicenseNumber} is not found"));
            }

            var route = new List<Coordinate>();
            int lat = request.Lat;
            while(lat != car.Lat)
            {
                if( lat > car.Lat)
                {
                    route.Add(new Coordinate(lat - 1, car.Lon));
                    lat--;
                }
                if(lat < car.Lat)
                {
                    route.Add(new Coordinate(lat + 1, car.Lon));
                    lat++;
                }
            }
            int lon = request.Lon;
            while (lon != car.Lon)
            {
                if (lon > car.Lon)
                {
                    route.Add(new Coordinate(car.Lat,lon - 1));
                    lon--;
                }
                if (lon < car.Lon)
                {
                    route.Add(new Coordinate(car.Lat, lon + 1));
                    lon++;
                }
            }
            return new CarRouteResult(car, route);
        }
    }
}

