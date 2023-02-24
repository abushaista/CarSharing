using System;
using CarSharing.Domain.Authentication;

namespace CarSharing.Domain.Repositories
{
	public interface IUserRepository
	{
        Task<User> GetUserByEmail(string email);
        Task Add(User user);
    }
}

