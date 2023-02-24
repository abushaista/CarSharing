using CarSharing.Domain.Shared;
using MediatR;
namespace CarSharing.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    
}