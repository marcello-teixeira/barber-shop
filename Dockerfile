FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY . .

RUN dotnet build -c Release

RUN dotnet publish -c release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /out .

ENTRYPOINT ["dotnet", "BarberShop_Api.dll"]
