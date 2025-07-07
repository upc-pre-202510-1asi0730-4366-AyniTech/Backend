using AyniTech.IAM.Application.QueryServices;
using Lot.IAM.Application.CommandServices;
using Lot.IAM.Application.OutBoundServices;
using Lot.IAM.Domain.Repositories;
using Lot.IAM.Domain.Services;
using Lot.IAM.Infrastructure.Hashing.BCrypt.Services;
using Lot.IAM.Infrastructure.Repositories;
using Lot.IAM.Infrastructure.Tokens.JWT.Configuration;
using Lot.IAM.Infrastructure.Tokens.JWT.Services;
using Lot.Inventaries.Application.Internal.CommandServices;
using Lot.Inventaries.Application.Internal.QuerysServices;
using Lot.Inventaries.Domain.Repositories;
using Lot.Inventaries.Domain.Services;
using Lot.Inventaries.Infraestructure.Persistence.EFC.Repositories;

using Lot.ProductManagement.Application.Internal.CommandServices;
using Lot.ProductManagement.Application.Internal.QueryServices;
using Lot.ProductManagement.Domain.Repositories;
using Lot.ProductManagement.Domain.Services;
using Lot.ProductManagement.Infrastructure.Persistence.EFC.Repositories;
using Lot.Shared.Domain.Repositories;
using Lot.Shared.Infrastructure.Persistence.EFC.Seeding;
using Lot.Shared.Infraestructure.ASP.Configuration.Extensions;
using Lot.Shared.Infraestructure.Persistence.EFC.Configuration.Extensions;
using Lot.Shared.Infraestructure.Persistence.EFC.Repositories;
using Lot.Reports.Domain.Repositories;
using Lot.Reports.Domain.Services;
using Lot.Reports.Application.Internal.CommandServices;
using Lot.Reports.Infrastructure.Persistence.EFC.Repositories;
using Lot.Reports.Application.Internal.QueryServices;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Lot.AlertStockManagement.Application.Internal.QueryServices; 
using Lot.AlertStockManagement.Domain.Repositories;
using Lot.AlertStockManagement.Infraestructure.Persistence.EFC.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configure JWT Authentication
var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
var key = Encoding.ASCII.GetBytes(tokenSettings?.Secret ?? "default-secret-key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero,
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
    
    options.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            Console.WriteLine("🔐 Token validado exitosamente");
            var claims = context.Principal.Claims;
            Console.WriteLine("📜 Claims del token:");
            foreach (var claim in claims)
            {
                Console.WriteLine($"   - {claim.Type}: {claim.Value}");
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("❌ Fallo en la autenticación del token:");
            Console.WriteLine($"   Error: {context.Exception.Message}");
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            Console.WriteLine("🚫 Desafío de autenticación:");
            Console.WriteLine($"   Error: {context.Error}");
            Console.WriteLine($"   Error Description: {context.ErrorDescription}");
            return Task.CompletedTask;
        }
    };
});

// Add Authorization
builder.Services.AddAuthorization();

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT token — escribe solo el token, el prefijo 'Bearer' se agrega automáticamente.",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,       // 👈 Tipo correcto
        Scheme = "bearer",                    // 👈 Esquema correcto
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
});
/*
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Lot API",
            Version = "v1",
            Description = "API para gestión de inventarios por lote",
            TermsOfService = new Uri("https://lot.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "Equipo Lot",
                Email = "contact@lot.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });

    // --- SEGURIDAD JWT PARA SWAGGER ---
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    // --- FIN SEGURIDAD JWT ---

    options.EnableAnnotations();
});
*/
// Dependency Injection

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWorks>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Inventaries Bounded Context
builder.Services.AddScoped<IInventoryByProductRepository, InventoryByProductRepository>();
builder.Services.AddScoped<IInventoryByBatchRepository, InventoryByBatchRepository>();

builder.Services.AddScoped<IInventoryByProductCommandService, InventoryByProductCommandService>();
builder.Services.AddScoped<IInventoryByBatchCommandService, InventoryByBatchCommandService>();

builder.Services.AddScoped<IInventoryByProductQueryService, InventoryByProductQueryService>();
builder.Services.AddScoped<IInventoryByBatchQueryService, InventoryByBatchQueryService>();

// ProductManagement Bounded Context
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IProductCommandService, ProductCommandService>();
builder.Services.AddScoped<IProductQueryService, ProductQueryService>();
builder.Services.AddScoped<IComboRepository, ComboRepository>(); 
builder.Services.AddScoped<IComboCommandService, ComboCommandService>();
builder.Services.AddScoped<IComboQueryService, ComboQueryService>();


// Registrar repositorios
builder.Services.AddScoped<IComboRepository, ComboRepository>();

// Registrar servicios
builder.Services.AddScoped<IComboCommandService, ComboCommandService>();
builder.Services.AddScoped<IComboQueryService, ComboQueryService>();


// Reports Bounded Context

builder.Services.AddScoped<IStockAverageReportCommandService, StockAverageReportCommandService>();
builder.Services.AddScoped<ICategoryReportCommandService, CategoryReportCommandService>();
builder.Services.AddScoped<IStockAverageReportQueryService, StockAverageReportQueryService>();
builder.Services.AddScoped<ICategoryReportQueryService, CategoryReportQueryService>();
builder.Services.AddScoped<IStockAverageReportRepository, StockAverageReportRepository>();
builder.Services.AddScoped<ICategoryReportRepository, CategoryReportRepository>();


// AlertStockManagement Bounded Context
builder.Services.AddScoped<IInventoryReadRepository, InventoryReadRepository>();
builder.Services.AddScoped<StockAlertQueryService>();

Console.WriteLine("Construyendo la aplicación...");
var app = builder.Build();
Console.WriteLine("Aplicación construida exitosamente");

// Verifica si la base de datos existe y créala si no existe
Console.WriteLine("Inicializando base de datos...");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    
    // Crear la base de datos si no existe
    Console.WriteLine("Creando base de datos si no existe...");
    context.Database.EnsureCreated();
    Console.WriteLine("Base de datos inicializada correctamente");
    
   
    try
    {
        Console.WriteLine("Iniciando seeding de datos de ejemplo...");
        await DataSeederService.SeedDataAsync(context);
        Console.WriteLine("Seeding de datos completado exitosamente");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error durante el seeding de datos: {ex.Message}");
        Console.WriteLine($"Detalles: {ex.InnerException?.Message}");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

// Importante: UseAuthentication debe ir ANTES de UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();