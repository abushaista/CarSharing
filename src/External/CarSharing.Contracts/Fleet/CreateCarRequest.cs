using System;
namespace CarSharing.Contracts.Fleet;

public record CreateCarRequest(string LicenseNumber, int Seat, float Price, int Lat, int Lon);


