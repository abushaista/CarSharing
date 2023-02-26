using System;
using System.Text;
using CarSharing.Application.Abstractions;
using CarSharing.Application.Order.Common;
using CarSharing.Domain.Fleet;
using CarSharing.Domain.Order;

namespace CarSharing.Application.Services
{
	public class BillCalculator : IBillCalculator
    {
        public OrderResult Calculate(Booking order, Car car)
        {
            TimeSpan span = order.EndDate.Value.Subtract(order.StartDate);
            var minutesBill = (span.Minutes * car.Price) / 60;
            var hoursBill = span.Hours * car.Price;
            var daysBill = span.Days * 24 * car.Price;
            StringBuilder sb = new StringBuilder();
            if(span.Days > 0)
            {
                sb.Append($"{span.Days} day(s) ");
            }
            if(span.Hours > 0)
            {
                sb.Append($"{span.Hours} hour(s) ");
            }
            if(span.Minutes > 0)
            {
                sb.Append($"{span.Minutes} min(s)");
            }
            return new OrderResult(order.Id, car.LicenseNumber, (daysBill + hoursBill + minutesBill), sb.ToString());
        }
    }
}

