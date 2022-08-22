using FluentValidation.AspNetCore;
using FSE.SkillTracker.Application.Behaviors;
using FSE.SkillTracker.Application.Features.Profile.Commands;
using FSE.SkillTracker.Application.Features.Skillset.Commands;
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

builder.Services.AddMediatR(typeof(CreateSkillsetCommand));
builder.Services.AddMediatR(typeof(CreateProfileCommand));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(FSE.SkillTracker.AddProfileApi.Middleware.ExceptionHandlingMiddleware));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped(typeof(IProfileRepository), typeof(ProfileRepository));
builder.Services.AddScoped(typeof(ISkillsetRepository), typeof(SkillsetRepository));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApiVersioning(options =>
{
    // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
    options.ReportApiVersions = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
