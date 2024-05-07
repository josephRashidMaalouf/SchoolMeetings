using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolMeetings.Domain.Entities.Authentication;
using SchoolMeetings.Infrastructure;
using SchoolMeetings.Infrastructure.DevelopmentUserDemoData;
using System.Security.Claims;
using SchoolMeetings.Api.DependencyInjection;
using SchoolMeetings.Api.Extensions;

//TODO: change to environmnet variable
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolMeetingsDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies();
builder.Services.AddAuthorizationBuilder();

//TODO: change to environmnet variable
builder.Services.AddDbContext<SchoolMeetingsDbContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
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


