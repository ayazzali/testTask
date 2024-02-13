FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Flights/Flights.csproj", "Flights/"]
RUN dotnet restore "Flights/Flights.csproj"
COPY . .
WORKDIR "/src/Flights"
RUN dotnet build "Flights.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Flights.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Flights.dll"]
