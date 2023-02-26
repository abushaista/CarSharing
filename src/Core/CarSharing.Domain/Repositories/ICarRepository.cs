using System;
using CarSharing.Domain.Fleet;

namespace CarSharing.Domain.Repositories
{
	public interface ICarRepository
	{
		Task<Car> GetCarById(Guid id);
		Task<Car> GetCarByLicenseNo(string LicenseNo);
		Task<List<Car>> GetAllActiveCarWithinRange(int Lan, int Lon);
		Task<bool> Update(Car car);
		Task Add(Car car);
		Task<Car> OrderNearby(int Lan, int Lon);
		Task<List<Car>> GetAllCars(string? LicenseNumber, int? Seat, float? MinPrice, float? MaxPrice, int Page, int Rows);


    }
}

