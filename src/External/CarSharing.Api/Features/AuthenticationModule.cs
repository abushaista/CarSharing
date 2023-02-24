using System;
using Carter;
using CarSharing.Contracts.Authentication;
using Mapster;
using CarSharing.Application.Authentication.Queries;
using MediatR;
using CarSharing.Application.Authentication.Commands;

namespace CarSharing.Api.Features;

public class AuthenticationModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/Login", async (LoginRequest request, ISender sender) =>
        {
            LoginQuery query = request.Adapt<LoginQuery>();
            var result = await sender.Send(query);
            if (result.IsFailure)
            {
                return Results.Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.Error.Message);
            }
            return Results.Ok(result);

        });

        app.MapPost("/Register", async (RegisterRequest request, ISender sender) =>
        {
            RegisterCommand command = request.Adapt<RegisterCommand>();
            var result = await sender.Send(command);
            if (result.IsFailure)
            {
                return Results.Problem(statusCode: StatusCodes.Status401Unauthorized, title: result.Error.Message);
            }
            return Results.Ok(result);
        });
    }
}


