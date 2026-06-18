using identity_service.Context;
using identity_service.Services.Contracts;
using identity_service.Services.Implementations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

string? connectionString = configuration.GetConnectionString("connectionString") ?? "";

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddAutoMapper(cfg => {}, typeof(Program))
    .AddPooledDbContextFactory<IdentityServiceDbContext>(o =>
        o.UseMySQL(connectionString)
    )
    .AddScoped<IGroupService, GroupService>()
    .AddScoped<IRefreshTokenService, RefreshTokenService>()
    .AddScoped<IRoleGroupService, RoleGroupService>()
    .AddScoped<IRoleService, RoleService>()
    .AddScoped<IUserGroupService, UserGroupService>()
    .AddScoped<IUserRoleService, UserRoleService>()
    .AddScoped<IUserService, UserService>()
    .AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Identity Service");
app.MapControllers();

// app.UseHttpsRedirection();



app.Run();