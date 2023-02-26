namespace CarSharing.Contracts.Fleet;

public record CarResponse(Guid Id, string LicenseNumber, int Seat, int Lat, int Lon);