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

    public async Task<List<Car>> GetAllCars(string? LicenseNumber, int? Seat, float? MinPrice, float? MaxPrice, int Page, int Rows)
    {
        var query = _dbContext.Set<Car>();
        if (!string.IsNullOrWhiteSpace(LicenseNumber))
        {
            query.Where(x => x.LicenseNumber.Contains(LicenseNumber));
        }
        if(Seat != null)
        {
            query.Where(x => x.Seat == Seat);
        }
        if(MinPrice != null)
        {
            query.Where(x => x.Price > MinPrice);
        }
        if(MaxPrice != null)
        {
            query.Where(x => x.Price < MaxPrice);
        }
        query.Skip(Page * Rows).Take(Rows).OrderBy(x => x.Id);
        return await query.ToListAsync();
    }
}