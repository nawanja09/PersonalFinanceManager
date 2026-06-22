using Finance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Finance.Application.Abstractions.Persistence;
using Finance.Application.Abstractions.Services;
using Finance.Infrastructure.Repositories;
using Finance.Infrastructure.Services;
using MediatR;
using Finance.Application.Features.Authentication.Register;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();