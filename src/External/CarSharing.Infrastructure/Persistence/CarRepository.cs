using CarSharing.Domain.Fleet;
using CarSharing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Infrastructure.Persistence;

public class CarRepository : ICarRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CarRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Car car)
    {
        _dbContext.Set<Car>().Add(car);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Car>> GetAllActiveCarWithinRange(int Lat, int Lon)
    {
        var data = await _dbContext.Set<Car>().Where(x => x.IsActive == true
        && x.Available == true && (Math.Abs(x.Lon - Lon) + Math.Abs(x.Lat - Lat)) <=2).ToListAsync();
        return data;
    }

    public async Task<Car> GetCarById(Guid id)
    {
        var data = await _dbContext.Set<Car>().Where(x => x.Id == id).FirstOrDefaultAsync();
        return data;
    }

    public async Task<Car> GetCarByLicenseNo(string LicenseNo)
    {
        var data = await _dbContext.Set<Car>().Where(x => x.LicenseNumber == LicenseNo).FirstOrDefaultAsync();
        return data;
    }

    public async Task<Car> OrderNearby(int Lat, int Lon)
    {
        var data = await _dbContext.Set<Car>().Where(x => x.IsActive == true && x.Available == true && (Math.Abs(x.Lon - Lon) + Math.Abs(x.Lat - Lat)) <= 2).FirstOrDefaultAsync();
        return data;
    }

    public async Task<bool> Update(Car car)
    {
        try
        {
            _dbContext.Set<Car>().Update(car);
            await _dbContext.SaveChangesAsync();
        }catch (DbUpdateConcurrencyException ex)
        {
            return false;
        }
        
        return true;
    }
}