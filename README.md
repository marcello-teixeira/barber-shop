<h1 align="center">
  <b>BarbShop - API</b>
  <img src="https://i.pinimg.com/originals/ce/2e/b5/ce2eb5c24ec4ea4a59ec9a82905765d8.png" width="50"> 
  <br>
</h1>

BarbShop - API is responsible for the connection between the <a href="https://github.com/marcello-teixeira/barber-shop-client">web page</a> and the SQLSERVER database.

# Description 📝

This is a RESTful API that follows standard CRUD operations. It also integrates a <a href="https://api.opencagedata.com">Geolocation API</a> and includes some methods for documents verification, handler tokens and generate DTOs.

# Languages and Tools 🛠️

- .NET 8.0
  <br>
  ° ASP.NET
  <br>
  ° Entity Framework Core (and tools)
  <br>
  ° Automapper
  <br>
  ° JWTBearer
  <br>
  ° dotenv.net
  <br>

- SQL SERVER 14.0

# Enviroment Variables

Tha enviroment variables are processed by docker-compose.yml. They are:

- API_KEY = API KEY of the Open Cage Data. 
- SA_PASSWORD = Database system administrator password 
- ACCEPT_EULA = Accept eula. Y or N.
- ConnectionStrings__SQLServerConnection = Server=Docker-Service-Name;Database=barbershop;User Id=sa;Password=*********;

