using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WebAPI_Test.Authentication;
using WebAPI_Test.Data;
using WebAPI_Test.Interfaces;
using WebAPI_Test.Login;
using WebAPI_Test.Models;
using WebAPI_Test.OptionsSetup;
using WebAPI_Test.Tools;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddScoped<LoginCommandHandler>();
builder.Services.AddScoped<IUsersRepository, SecretDbContext>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher<User>, UserPasswordHasher>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

//builder is the Dependency Injection thingy
builder.Services.AddDbContext<SecretDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresServer")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();

app.UseAuthorization();

app.Run();