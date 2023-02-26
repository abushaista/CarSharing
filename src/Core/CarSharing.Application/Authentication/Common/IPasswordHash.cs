using System;
namespace CarSharing.Application.Authentication.Common
{
	public interface IPasswordHash
	{
		string Generate(string value);
		bool Verify(string value, string hashedValue);
	}
}

