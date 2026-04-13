FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["Backend.sln", "."]
COPY ["Restaurant.System.Api/Restaurant.System.Api.csproj", "Restaurant.System.Api/"]
COPY ["Restaurant.System.Controllers/Restaurant.System.Controllers.csproj", "Restaurant.System.Controllers/"]
COPY ["Restaurant.System.Data/Restaurant.System.Data.csproj", "Restaurant.System.Data/"]
COPY ["Restaurant.System.Models/Restaurant.System.Models.csproj", "Restaurant.System.Models/"]
COPY ["Restaurant.System.Services/Restaurant.System.Services.csproj", "Restaurant.System.Services/"]

# Restore dependencies
RUN dotnet restore "Backend.sln"

# Copy source code
COPY . .

# Build and publish
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080

# Copy published application
COPY --from=build /app/publish .

# Set environment to production
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Restaurant.System.Api.dll"]