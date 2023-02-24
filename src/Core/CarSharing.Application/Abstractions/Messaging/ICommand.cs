﻿using CarSharing.Domain.Shared;
using MediatR;
namespace CarSharing.Application.Abstractions.Messaging;

public interface ICommand: IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }


