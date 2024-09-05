using BarberShop_Api.Domain.Repositories;
using BarberShop_Api.Infrastructure;
using BarberShop_Api.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using BarberShop_Api.Application.Services;
using BarberShop_Api.Application.Mapping;
using System.Text;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using BarberShop_Api.Application.SwaggerOptions;
using Microsoft.EntityFrameworkCore.Infrastructure;

// Get a random key
byte[] key = Encoding.Default.GetBytes(GenerateKey.Private);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.DefaultApiVersion = new ApiVersion(1, 0);


}).AddMvc().AddApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureOptions<ConfigSwaggerGenOptions>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.OperationFilter<SwaggerDefaultValues>();

    x.AddSecurityDefinition(
        "Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer",
            Type = SecuritySchemeType.ApiKey
        });

    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//
// Entities's DTOs 
//

builder.Services.AddAutoMapper(typeof(CompanyMapping));
builder.Services.AddAutoMapper(typeof(CustomerMapping));
builder.Services.AddAutoMapper(typeof(HaircutMapping));
builder.Services.AddAutoMapper(typeof(OrdersMapping));



builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "DefaultPolicy", policy =>
    {
        policy.WithOrigins("https://webpage-barbershop.azurewebsites.net");
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    
}).AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = true;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});                      


builder.Services.AddDbContext<ConnectionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

//
// Initial migration 
//

using (var scope = app.Services.CreateScope())
{
    var DbContext = scope.ServiceProvider.GetRequiredService<ConnectionContext>();
    DbContext.Database.Migrate();
}

//
// Configure the Swagger Versionament and UI
//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        var apiInfo = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach(var info in apiInfo.ApiVersionDescriptions)
        {
            opt.SwaggerEndpoint($"/swagger/{info.GroupName}/swagger.json", $"BarberShop - {info.GroupName}");
        }
    });

}

app.UseAuthorization();

app.UseCors("DefaultPolicy");

app.MapControllers();

app.Run();
