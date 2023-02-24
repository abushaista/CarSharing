using CarSharing.Application.Abstractions.Messaging;
using CarSharing.Application.Authentication.Common;
using CarSharing.Domain.Repositories;
using CarSharing.Domain.Shared;

namespace CarSharing.Application.Authentication.Queries;

public class LoginQueryHandler : IQueryHandler<LoginQuery, AuthenticationResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly IPasswordHash _hash;
    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator, IPasswordHash hash)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _hash = hash;
    }
    public async Task<Result<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        var password = _hash.Generate(request.Password);
        if(user == null || user.Password != password)
        {
            return (Result<AuthenticationResult>)Result.Failure(new Error("401", "Invalid Credential"));
        }
        var token = _tokenGenerator.GenerateToken(user);
        return new AuthenticationResult(user, token);
    }
}