using System;
namespace CarSharing.Application.Order.Common;

public sealed record OrderResult(Guid Id,string LicenseNumber, float Total, string Duration);


