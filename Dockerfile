FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MiniWbs.csproj", "./"]
RUN dotnet restore "MiniWbs.csproj"


COPY . .
RUN dotnet build "MiniWbs.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "MiniWbs.csproj" -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .


ENV ASPNETCORE_URLS=http://+:${PORT:-8080}


RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


ENTRYPOINT ["dotnet", "MiniWbs.dll"]