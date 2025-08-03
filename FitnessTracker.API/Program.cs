using FitnessTracker.API.Middlewares;
using FitnessTracker.Application.Interfaces.Auth;
using FitnessTracker.Application.Interfaces.Workouts;
using FitnessTracker.Application.Mappers;
using FitnessTracker.Application.UseCases.Auth;
using FitnessTracker.Application.UseCases.Workouts;
using FitnessTracker.Application.Validators.Auth;
using FitnessTracker.Domain.Interfaces.Repositories;
using FitnessTracker.Domain.Interfaces.Services;
using FitnessTracker.Infrastructure;
using FitnessTracker.Infrastructure.Repositories;
using FitnessTracker.Infrastructure.Services;
using FitnessTracker.Infrastructure.Services.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fitness Tracker API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//Db
builder.Services.AddDbContext<FitnessDbContext>(options => {
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(nameof(FitnessDbContext))
        );
});

//Settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<JwtSettings>>().Value);

// Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(RegisterValidator));

//Mappers
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<WorkoutProfile>();
    cfg.AddProfile<ExerciseProfile>();
    cfg.AddProfile<SetProfile>();
});

//Repos
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();

//Services
builder.Services.AddScoped<IPasswordService, BCryptPasswordService>();
builder.Services.AddScoped<ITokensService, JwtTokenService>();


//UseCases
//Auth
builder.Services.AddScoped<IRegisterUseCase, RegisterUseCase>();
builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
builder.Services.AddScoped<IRefreshTokensUseCase, RefreshTokensUseCase>();
//Workouts
builder.Services.AddScoped<ICreateWorkoutUseCase, CreateWorkoutUseCase>();
builder.Services.AddScoped<IGetAllWorkoutsUseCase, GetAllWorkoutsUseCase>();
builder.Services.AddScoped<IGetWorkoutByIdUseCase, GetWorkoutByIdUseCase>();
builder.Services.AddScoped<IDeleteWorkoutUseCase, DeleteWorkoutUseCase>();
//jwt

var jwtSettings = builder.Configuration
    .GetSection(nameof(JwtSettings))
    .Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings!.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

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
