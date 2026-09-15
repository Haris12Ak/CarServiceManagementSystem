using Microsoft.OpenApi;

namespace API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(
        this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                var baseUrl = builder.Configuration["Keycloak:BaseUrl"]!;
                var realm = builder.Configuration["Keycloak:Realm"]!;

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Car Service",
                    Version = "v1"
                });

                options.AddSecurityDefinition(
                    "Keycloak",
                    new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.OAuth2,

                        Flows = new OpenApiOAuthFlows
                        {
                            AuthorizationCode = new OpenApiOAuthFlow
                            {
                                AuthorizationUrl = new Uri($"{baseUrl}/realms/{realm}/protocol/openid-connect/auth"),
                                TokenUrl = new Uri($"{baseUrl}/realms/{realm}/protocol/openid-connect/token"),
                                Scopes = new Dictionary<string, string>
                                {
                                    { "openid", "OpenID Connect scope" },
                                    { "profile", "User profile" }
                                }
                            }
                        }
                    });

                options.AddSecurityRequirement(doc =>
                new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecuritySchemeReference("Keycloak", doc), []
                    }
                });

            });

            return builder;
        }
    }
}
