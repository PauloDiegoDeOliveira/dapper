using Empresa.Dapper.API.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

// Services

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configurationManager = builder.Configuration;

IWebHostEnvironment environment = builder.Environment;

builder.Services.AddControllers();

builder.Services.AddFluentValidationConfiguration();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddDatabaseConfiguration(configurationManager);

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddCorsConfiguration();

builder.Services.AddVersionConfiguration();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Configure services

var app = builder.Build();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseCors("Development");
}
else if (app.Environment.IsStaging())
{
    app.UseCors("Staging");
}
else if (app.Environment.IsProduction())
{
    app.UseCors("Production");
    app.UseHsts();
}

app.UseDatabaseConfiguration();

app.UseSwaggerConfiguration(environment, provider);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();