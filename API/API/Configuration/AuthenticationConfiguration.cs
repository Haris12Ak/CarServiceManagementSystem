using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace API.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static WebApplicationBuilder AddKeycloakAuthentication(
            this WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var baseUrl = builder.Configuration["Keycloak:BaseUrl"]!;
                    var realm = builder.Configuration["Keycloak:Realm"]!;

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.MetadataAddress =
                        builder.Configuration["Authentication:MetadataAddress"]!;
                    options.Audience =
                        builder.Configuration["Keycloak:Audience"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudiences = new[]
                        {
                            "api-client",
                            "account"
                        },
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = $"{baseUrl}/realms/{realm}",
                        IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                        {
                            using var client = new HttpClient();
                            var keyUri = $"{baseUrl}/realms/{realm}/protocol/openid-connect/certs";
                            var response = client.GetAsync(keyUri).Result;
                            var keys = new JsonWebKeySet(response.Content.ReadAsStringAsync().Result);
                            return keys.GetSigningKeys();
                        },

                        IssuerValidator = (issuer, token, parameters) =>
                        {
                            var validIssuers = $"{baseUrl}/realms/{realm}";
                            if (validIssuers.Contains(issuer))
                                return issuer;
                            throw new SecurityTokenInvalidIssuerException($"Invalid issuer: {issuer}");
                        }
                    };
                });
            return builder;
        }
    }
}
