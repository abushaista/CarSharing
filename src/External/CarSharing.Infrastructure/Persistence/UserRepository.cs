using CarSharing.Domain.Authentication;
using CarSharing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;
     public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        _dbContext.Set<User>().Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var data = await _dbContext.Set<User>().Where(x => x.Email == email).FirstOrDefaultAsync();
        return data;
    }
}