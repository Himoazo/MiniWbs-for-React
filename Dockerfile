# Use the official ASP.NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore "MiniWbs/MiniWbs.csproj"

# Build the application
RUN dotnet build "MiniWbs/MiniWbs.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "MiniWbs/MiniWbs.csproj" -c Release -o /app/publish

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the port the app runs on
EXPOSE 8080

# Set non-root user for security
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# Start the app
ENTRYPOINT ["dotnet", "MiniWbs.dll"]