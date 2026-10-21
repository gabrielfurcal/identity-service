using System.Text;
using identity_service.Context;
using identity_service.Services.Contracts;
using identity_service.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

string? connectionString = configuration.GetConnectionString("connectionString") ?? "";

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAdminInputs",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });

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
app.UseCors("AllowAdminInputs");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "Identity Service");
app.MapControllers();

// app.UseHttpsRedirection();



app.Run();