using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolMeetings.Domain.Entities.Authentication;
using SchoolMeetings.Infrastructure;
using SchoolMeetings.Infrastructure.DevelopmentUserDemoData;
using System.Security.Claims;
using SchoolMeetings.Api.DependencyInjection;
using SchoolMeetings.Api.Extensions;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//TODO: finish google auth
builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

//TODO: Update to live connectionstring when the database is hosted
builder.Services.AddDbContext<SchoolMeetingsDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration["Database:Local"]);
    });

builder.Services.AddIdentityCore<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<SchoolMeetingsDbContext>()
    .AddApiEndpoints();



builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "http://localhost:5161",
                builder.Configuration["FrontendUrl"] ?? "http://localhost:5241"])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await using var scope = app.Services.CreateAsyncScope();
    await SeedData.InitializeAsync(scope.ServiceProvider);
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.UseCors("wasm");

app.UseAuthentication();
app.UseAuthorization();

app.MapAllEndPoints();




app.Run();


