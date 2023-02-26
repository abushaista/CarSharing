using System.Reflection;

namespace CarSharing.Application;
public static class ApplicationAssembly
{
    public static readonly Assembly Instance = typeof(ApplicationAssembly).Assembly;
}