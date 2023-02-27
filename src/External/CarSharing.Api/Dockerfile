#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/External/CarSharing.Api/CarSharing.Api.csproj", "src/External/CarSharing.Api/"]
COPY ["src/External/CarSharing.Contracts/CarSharing.Contracts.csproj", "src/External/CarSharing.Contracts/"]
COPY ["src/Core/CarSharing.Application/CarSharing.Application.csproj", "src/Core/CarSharing.Application/"]
COPY ["src/Core/CarSharing.Domain/CarSharing.Domain.csproj", "src/Core/CarSharing.Domain/"]
COPY ["src/External/CarSharing.Infrastructure/CarSharing.Infrastructure.csproj", "src/External/CarSharing.Infrastructure/"]
RUN dotnet restore "src/External/CarSharing.Api/CarSharing.Api.csproj"
COPY . .
WORKDIR "/src/src/External/CarSharing.Api"
RUN dotnet build "CarSharing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarSharing.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarSharing.Api.dll"]
