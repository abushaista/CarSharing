using System;
using System.Reflection;
using System.Security.Claims;
using CarSharing.Application.Fleet.Commands;
using CarSharing.Application.Fleet.Queries;
using CarSharing.Contracts.Fleet;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace CarSharing.Api.Features
{
    public class FleetModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/fleet/{lat}/{lon}", async (HttpRequest req, int lat, int lon, ISender sender) =>
            {
                GetNearbyFleetQuery query = new GetNearbyFleetQuery(lat, lon);
                var result = await sender.Send(query);
                var data = result.Value.Adapt<List<CarResponse>>();
                return Results.Ok(data);
            });
            app.MapPost("/fleet", [Authorize] async (CreateCarRequest request,ISender sender) =>
            {
                CreateCarCommand command = request.Adapt<CreateCarCommand>();
                var result = await sender.Send(command);
                var data = result.Value.Adapt<CarResponse>();
                return Results.Ok(data);
            }).RequireAuthorization("admin");
            app.MapGet("/fleet/{lat}/{lon}/base/{licenseNumber}", async (string licenseNumber, int lat, int lon, ISender sender) =>
            {
                GetFleetBaseRouteQuery query = new GetFleetBaseRouteQuery(licenseNumber, lat, lon);
                var result = await sender.Send(query);
                if (result.IsFailure)
                {
                    return Results.NotFound(result.Error.Message);
                }
                
                var data = result.Value.Adapt<CarRouteResponse>();
                return Results.Ok(data);
            });
        }
    }
}

