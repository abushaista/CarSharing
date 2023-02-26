using CarSharing.Domain.Order;
using CarSharing.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Infrastructure.Persistence;

public class BookingRepository : IBookingRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Add(Booking booking)
    {
        _dbContext.Set<Booking>().Add(booking);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Booking> GetBookingByCarId(Guid Id)
    {
        var data = await _dbContext.Set<Booking>().Where(x => x.CarId == Id && x.EndDate == null).FirstOrDefaultAsync();
        return data;
    }

    public async Task<Booking> GetBookingById(Guid id)
    {
        var data = await _dbContext.Set<Booking>().Where(x => x.Id == id).FirstOrDefaultAsync();
        return data;
    }

    public async Task<bool> Update(Booking booking)
    {
        booking.EndDate = DateTime.UtcNow;
        _dbContext.Set<Booking>().Update(booking);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}