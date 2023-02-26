using System;
namespace CarSharing.Contracts.Fleet;

public record CreateCarRequest(string LicenseNumber, int Seat, int Lat, int Lon);


