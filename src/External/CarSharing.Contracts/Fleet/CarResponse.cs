namespace CarSharing.Contracts.Fleet;

public record CarResponse(Guid Id, string LicenseNumber, int Seat, float Price, int Lat, int Lon);