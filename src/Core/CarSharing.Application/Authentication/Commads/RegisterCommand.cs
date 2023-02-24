using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Authentication.Common;

namespace CarSharing.Application.Authentication.Commands;

public record RegisterCommand(string FirstName, 
    string LastName, 
    string Email, 
    string Password) : ICommand<AuthenticationResult>;