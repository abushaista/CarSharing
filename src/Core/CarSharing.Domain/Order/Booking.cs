using CarSharing.Domain.Authentication;
using CarSharing.Domain.Fleet;

namespace CarSharing.Domain.Order;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public virtual User? User {get;set;}
    public Guid CarId { get; set; }
    public virtual Car? Car { get; set; }
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; }

}