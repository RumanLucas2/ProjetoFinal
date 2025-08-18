using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ProjetoFinal API",
        Version = "v1",
        Description = "API do ProjetoFinal com endpoints de Loja."
    });

    // XML comments (habilite no .csproj, item 2)
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

    // (Opcional) Auth via JWT Bearer no header Authorization
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

    // (Opcional) Adicionar headers 'login' e 'senha' globalmente (ver classe no passo 3)
    // c.OperationFilter<AddLoginSenhaHeaders>();
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Swagger em qualquer ambiente (se preferir só em Dev, coloque dentro do if)
app.UseSwagger();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjetoFinal API v1");
    opt.RoutePrefix = "";
});

// Se deixar esse fallback, ele pode competir com a raiz:

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
