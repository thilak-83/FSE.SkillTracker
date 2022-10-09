using FluentValidation.AspNetCore;
using FSE.SkillTracker.Application.Behaviors;
using FSE.SkillTracker.Application.Features.Profile.Queries;
using FSE.SkillTracker.Application.Intefaces;
using FSE.SkillTracker.Application.Interfaces;
using FSE.SkillTracker.Application.Validators;
using FSE.SkillTracker.Domain.Configurations;
using FSE.SkillTracker.Infrastructure.Repostitories;
using FSE.SkillTracker.Infrastructure.Repostitories.Base;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(s =>
{
    s.RegisterValidatorsFromAssemblyContaining<CreateProfileCommandValidator>();
    s.RegisterValidatorsFromAssemblyContaining<SkillExpertiseValidator>();
});
ConfigurationManager configuration = builder.Configuration;

CosmosOptions cosmosConfig = configuration.GetSection(key: nameof(CosmosOptions)).Get<CosmosOptions>();
builder.Services.AddSingleton<ICosmosContainerFactory>(new CosmosContainerFactory(cosmosConfig.EndpointUrl, cosmosConfig.AuthKey, cosmosConfig.DatabaseName, cosmosConfig.Containers));

builder.Services.AddMediatR(typeof(GetProfilesByCriteriaQuery));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(FSE.SkillTracker.UpdateProfileApi.Middleware.ExceptionHandlingMiddleware));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));
builder.Services.AddScoped(typeof(ISkillsetRepository), typeof(SkillsetRepository));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning(options =>
{
    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
    options.ReportApiVersions = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//services cors
builder.Services.AddCors(options => options.AddPolicy("corsapp", policy =>
{
    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
