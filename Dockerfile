FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

WORKDIR /app

COPY . . 

RUN dotnet build

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /out

COPY --from=build /app/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "BarberShop_Api.dll"]
