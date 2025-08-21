using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// 🔑 suas classes (ajuste o namespace se diferente)
using ProjetoFinal.Server.Services; // JwtOptions / TokenService
using ProjetoFinal.Server.Services.Conn;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
// usando seus namespaces onde estiverem ITenantResolver, TenantResolver, ITenantConnectionAccessor, AppDbContext

var builder = WebApplication.CreateBuilder(args);

// =================== CONFIGURAÇÕES ===================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// JWT options (lendo de appsettings: "Jwt": { ... })
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;
var keyBytes = Encoding.UTF8.GetBytes(jwt.Key);

// multi-tenant (acessores / resolutores)
builder.Services.AddHttpContextAccessor();

// serviço de emissão de token (caso use DI no seu controller de login)
builder.Services.AddSingleton<TokenService>();

// =================== AUTENTICAÇÃO / AUTORIZAÇÃO ===================
builder.Services.AddScoped<ITenantConnectionAccessor, TenantConnectionAccessor>();
builder.Services.AddSingleton<IBancoResolver, BancoResolverInMemory>();

// Auth + JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        o.Events = new JwtBearerEvents
        {
            OnTokenValidated = ctx =>
            {
                // 1) Extrai o userId (sub) do token
                var userId =
                    ctx.Principal?.FindFirstValue(JwtRegisteredClaimNames.Sub) ??
                    ctx.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrWhiteSpace(userId))
                {
                    ctx.Fail("Token sem 'sub'.");
                    return Task.CompletedTask;
                }

                // 2) Resolve o nome do banco a partir do appsettings (BancoMap)
                var resolver = ctx.HttpContext.RequestServices.GetRequiredService<IBancoResolver>();
                var dbName = resolver.Resolve(userId) ?? "AppDev"; // fallback enquanto mapeia todos

                // 3) Monta a connection string e guarda pro resto da requisição
                var accessor = ctx.HttpContext.RequestServices.GetRequiredService<ITenantConnectionAccessor>();
                accessor.ConnectionString = ConnStringHelper.BuildTenantConnString(builder.Configuration, dbName);

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// =================== SWAGGER ===================
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProjetoFinal API",
        Version = "v1",
        Description = "API do ProjetoFinal com endpoints de Loja."
    });

    // XML comments (habilite no .csproj: <GenerateDocumentationFile>true</GenerateDocumentationFile>)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    // Botão Authorize com Bearer JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT no header Authorization. Ex.: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
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

    // (Opcional) Adicionar headers 'login' e 'senha' globalmente (se for usar operação custom)
    // c.OperationFilter<AddLoginSenhaHeaders>();
});

var app = builder.Build();

// =================== PIPELINE ===================
app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger em qualquer ambiente (se preferir só em Dev, coloque dentro do if)
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoFinal API v1");
    opt.RoutePrefix = ""; // Swagger na raiz
});

app.UseHttpsRedirection();

// **IMPORTANTE**: autenticação deve vir ANTES de autorização
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
