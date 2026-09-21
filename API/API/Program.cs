using API.Configuration;
using Application.Configuration;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddPersistenceRepositories(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddEndpointsApiExplorer();

builder.AddSwaggerConfiguration();

builder.AddKeycloakAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId(builder.Configuration["Keycloak:ClientId"]!);
        options.OAuthUsePkce();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
