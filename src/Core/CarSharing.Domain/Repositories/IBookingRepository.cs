using CarSharing.Domain.Order;

namespace CarSharing.Domain.Repositories;

public interface IBookingRepository 
{
    Task<bool> Add(Booking booing);
    Task<bool> Update(Booking booking);
    Task<Booking> GetBookingById(Guid id);
    Task<Booking> GetBookingByCarId(Guid Id);
}