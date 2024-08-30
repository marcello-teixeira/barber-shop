using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop_Api.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CNPJ = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Haircut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Haircut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaircutID = table.Column<int>(type: "int", nullable: false),
                    HaircutName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HaircutCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HaircutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HaircutDone = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

                migrationBuilder.InsertData(
                    table: "Customer",
                    columns: ["Id","Name","CPF","Photo","Email","Password","Phone"],
                    values:
                    [
                        1, "exampleCustomer", "11111111111", "Storage/profileDefault.png", "example@example.com", "932f3c1b56257ce8539ac269d7aab42550dacf8818d075f0bdf1990562aae3ef", "11987654321"
                    ]
                );

                migrationBuilder.InsertData(
                    table: "Company",
                    columns: ["Id","Name","Location","CNPJ","Photo","Email","Phone","Password"],
                    values:
                    [
                        1, "exampleCompany", "Ribeirao Preto, Sao Paulo, Brazil", "12345678900001", "Storage/profileDefault.png", "example@example.com", "11222223333","932f3c1b56257ce8539ac269d7aab42550dacf8818d075f0bdf1990562aae3ef"
                    ]
                );

                migrationBuilder.InsertData(
                    table: "Haircut",
                    columns: ["Id","Name","Cost","CompanyID"],
                    values: new object[,]
                    {
                        {1, "HaircutOne", 13.99, 1},
                        {2, "HaircutTwo", 23.99, 1}
                    }
                );

                migrationBuilder.InsertData(
                    table: "Orders",
                    columns: ["Id","CustomerID","CustomerName","CustomerPhone","CompanyID","CompanyName","CompanyPhone","CompanyLocation","HaircutID","HaircutName","HaircutCost","HaircutDate","HaircutDone"],
                    values: new object[,]
                    {
                        {1, "1", "exampleCustomer", "11987654321", 1, "exampleCompany", "11222223333", "Ribeirao Preto, Sao Paulo, Brazil", 1, "HaircutOne", 13.99, new DateTime(2024-12-09) , false},
                        {2, "1", "exampleCustomer", "11987654321", 1, "exampleCompany", "11222223333", "Ribeirao Preto, Sao Paulo, Brazil", 2, "HaircutTwo", 23.99, new DateTime(2024-12-05), false}
                    }
                );

                
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Haircut");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
