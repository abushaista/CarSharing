using System.ComponentModel.DataAnnotations;

namespace CarSharing.Domain.Fleet;

public class Car
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string LicenseNumber { get; set; } = string.Empty;
    public int Seat { get; set; } = 2;
    public int Lat { get; set; }
    public int Lon { get; set; }
    [ConcurrencyCheck]
    public bool Available { get; set; } = true;
    public bool IsActive { get; set; } = true;
}