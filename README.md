<h1 align="center">
  <b>BarbShop - API</b>
  <img src="https://i.pinimg.com/originals/ce/2e/b5/ce2eb5c24ec4ea4a59ec9a82905765d8.png" width="50"> 
  <br>
</h1>

BarbShop - API is responsible for the connection between the <a href="https://github.com/marcello-teixeira/barber-shop-client">web page</a> and the SQLServer database.
<br><br>
Swagger documentation: https://api-barbershop.azurewebsites.net/swagger.

## Summary

- [Description](#description)
- [Languages and Tools](#languages-and-tools)
- [Deployment](#deployment)
- [Installation](#installation)

## Description 📝

This is a RESTful API that follows standard CRUD operations. It also integrates a <a href="https://api.opencagedata.com">Geolocation API</a> and includes some methods for documents verification, handler tokens and generate DTOs.

## Language and Technologies 🛠️

- C#

- .NET 8.0
  - ASP.NET Core: A framework to building web applications.
  - Entity Framework Core: An ORM (Objective-Relational Mapping) that allows you to developed with object classes and map them to the entities of database.  
  - Automapper: Mapping models/entities and create DTOs.
  - JWTBearer: Token authentication.
  - dotenv.net: Initialize environment variables and get them.
  - Asp.Versioning: Allow versioning API.

- SQL SERVER 14.0

## Deployment ☁️

The database and API are hosted by Azure.

The enviroment variables are:

- API_KEY = Create an acconut at <a href="https://api.opencagedata.com">Open Cage Data</a> to get api key. 
- ConnectionStrings__SQLServerConnection = Server={server};Initial Catalog={database};User Id={user};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;

## Installation ⚙️

1. Clone the repository.
2. Check Docker are installed.
3. Create a file named setup.env in the root like .env.example and add environment variables.
4. Run command: docker compose --env-file setup.env up in the terminal
5. The API will be available at http://localhost:8080 and documentation at http://localhost:8080/swagger.
