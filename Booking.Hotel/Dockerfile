
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["/Booking.Hotel.API/Booking.Hotel.API.csproj", "Booking.Hotel.API/"]
COPY ["/Booking.Hotel.Data/Booking.Hotel.Data.csproj", "Booking.Hotel.Data/"]
COPY ["/Booking.Hotel.Domain/Booking.Hotel.Domain/Booking.Hotel.Domain.csproj", "Booking.Hotel.Domain/"]
RUN dotnet restore "Booking.Hotel.API/Booking.Hotel.API.csproj"
COPY . .
WORKDIR "/src/Booking.Hotel.API"
RUN dotnet build "Booking.Hotel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Booking.Hotel.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /app/publish/Booking.Hotel.API .
ENTRYPOINT ["dotnet", "Booking.Hotel.API.dll"]