using System;
using CarSharing.Application.Authentication.Common;
using Crypto = BCrypt.Net.BCrypt;

namespace CarSharing.Infrastructure.Authentication
{
	public class PasswordHash : IPasswordHash
    {
		public PasswordHash()
		{
		}

        public string Generate(string value)
        {
            return Crypto.HashPassword(value);
        }

        public bool Verify(string value, string hashedValue)
        {
            return Crypto.Verify(value, hashedValue);
        }
    }
}

