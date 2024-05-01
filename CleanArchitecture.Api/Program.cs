using CleanArchitecture.Api;
using CleanArchitecture.Api.Endpoints.Extensions;
using CleanArchitecture.Application;
using CleanArchitecture.Application.FeatureFlags;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Infrastructure.FeatureFlags;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

const string url = "https://e510-41-90-66-230.ngrok-free.app";

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // c.AddServer(new OpenApiServer
    // {
    //     Url = url,
    //     Description = "Local port forwarded to ngrok"
    // });
    c.OperationFilter<CustomOperationIdFilter>();
});
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());
app.UseStaticFiles();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture.Api v1");
    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
}).UseSwagger();
app.UseHttpsRedirection();
app.MapEndpoints();

app.SeedFeatureFlags();

app.Run();
