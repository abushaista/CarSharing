using System;
using CarSharing.Domain.Authentication;

namespace CarSharing.Application.Authentication.Common
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(User user);
	}
}

