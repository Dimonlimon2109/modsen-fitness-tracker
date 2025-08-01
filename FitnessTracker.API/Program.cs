using FitnessTracker.Application.Interfaces.Auth;
using FitnessTracker.Application.UseCases.Auth;
using FitnessTracker.Application.Validators.Auth;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;
using FitnessTracker.Infrastructure;
using FitnessTracker.Infrastructure.Repositories;
using FitnessTracker.Infrastructure.Services;
using FitnessTracker.Infrastructure.Services.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Db
builder.Services.AddDbContext<FitnessDbContext>(options => {
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(nameof(FitnessDbContext))
        );
});

//Settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegisterValidator));

//Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();


//Services
builder.Services.AddScoped<IPasswordService, BCryptPasswordService>();


//UseCases
//Auth
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();

app.Run();
