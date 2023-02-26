using System;
using CarSharing.Application.Abstractions;
using CarSharing.Domain.Order;

namespace CarSharing.Infrastructure.Notifications
{
	public class BillService : IBillService
    {
		public BillService()
		{
		}

        

        public async Task<bool> GenerateBill(Booking Order, Guid UserId, float Total)
        {
            //this implementation will be change on prod using async communication with other bill service
            await Task.CompletedTask;
            return true;
        }
    }
}

