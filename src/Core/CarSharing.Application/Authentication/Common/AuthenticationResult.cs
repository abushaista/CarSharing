using System;
using CarSharing.Domain.Authentication;

namespace CarSharing.Application.Authentication.Common;

public sealed record AuthenticationResult(User User, string Token);
	
	


