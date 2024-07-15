using JWT_Bearer_Token.DTO;
using JWT_Bearer_Token.FakeDatabase;
using JWT_Bearer_Token.Interface;
using JWT_Bearer_Token.JWT.Models;
using JWT_Bearer_Token.JWT.Policy;
using JWT_Bearer_Token.JWT.Services;
using JWT_Bearer_Token.Repository;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


var builder = WebApplication.CreateBuilder(args);

// SETUP
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));
builder.Services.AddScoped<IAuthentificationCustomRepository, AuthentificationCustomRepository>();
builder.Services.AddScoped<JWTGenerationService>();
builder.Services.AddScoped<JWTGetClaimsService>();
builder.Services.AddHttpContextAccessor();

JWTConfigurationCustomService.AddAuthentication(builder);
HandlerPolicy.AddAuthorization(builder);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
// ****


var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication(); // <---
app.UseAuthorization(); // <---




app.MapGet("/freeAccess", () => TypedResults.Ok(MockupDatabase.GetDataWeather())); // WITHOUT AUTHENTICATION
app.MapGet("/userOrAdminAccess", [Authorize(Policy = "UserOrAdmin")] () => TypedResults.Ok(MockupDatabase.GetDataWeather())); // AUTHENTICATION ROLE USER OR ADMIN 
app.MapGet("/adminAccess", [Authorize(Policy = "AdminOnly")] () => TypedResults.Ok(MockupDatabase.GetDataWeather())); // AUTHENTICATION ROLE ADMIN ONLY

app.MapGet("/log", ([FromServices] IAuthentificationCustomRepository authenticationService, [FromBody] UserLogDto log)
    => authenticationService.Authentification(log.Mail, log.Password)); // GET AUTHORISATION

app.Run();
