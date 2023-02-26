using System;
using System.Security.Claims;
using CarSharing.Application.Order.Commands;
using CarSharing.Domain.Authentication;
using Carter;
using MediatR;
using Mapster;
using CarSharing.Contracts.Fleet;
using Microsoft.AspNetCore.Authorization;

namespace CarSharing.Api.Features
{
    public class OrderModule : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/Order/{lat:int}/{lon:int}", [Authorize] async (HttpRequest req, int lat, int lon,ISender sender) =>
            {
                var identity = req.HttpContext.User.Identity as ClaimsIdentity;
                if(identity != null)
                {
                    var userClaims = identity.Claims;
                    var email = userClaims.First(x => x.Type == ClaimTypes.Email).Value;
                    CreateBookingCommand command = new CreateBookingCommand(lat, lon, email);
                    var data = await sender.Send(command);
                    if(data.IsFailure)
                    {
                        return Results.NotFound(data.Error.Message);
                    }
                    var result = data.Value.Adapt<CarResponse>();
                    return Results.Ok(result);
                }
                return Results.NotFound();
            });
        }
    }
}

