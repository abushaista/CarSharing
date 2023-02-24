using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Authentication.Common;
using CarSharing.Domain.Shared;
using MediatR;

namespace CarSharing.Application.Authentication.Queries;

public sealed record LoginQuery(string Email, string Password) : IQuery<AuthenticationResult>;

