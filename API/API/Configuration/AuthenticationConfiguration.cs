using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Json;

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

                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var identity = context?.Principal?.Identity as ClaimsIdentity;
                            if (identity == null) return Task.CompletedTask;

                            var realmAccess = context?.Principal?.FindFirst("realm_access");
                            if (realmAccess != null)
                            {
                                using var doc = JsonDocument.Parse(realmAccess.Value);
                                if (doc.RootElement.TryGetProperty("roles", out var roles))
                                {
                                    foreach (var role in roles.EnumerateArray())
                                    {
                                        var roleName = role.GetString();
                                        if (!string.IsNullOrWhiteSpace(roleName))
                                            identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                                    }
                                }
                            }

                            return Task.CompletedTask;
                        },

                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("JWT Validation failed: " + context.Exception);
                            return Task.CompletedTask;
                        }
                    };
                });
            return builder;
        }
    }
}
