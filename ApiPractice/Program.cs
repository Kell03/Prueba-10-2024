using ApiPractice.Auth;
using ApiPractice.EndPoints;
using Capa_Acceso_Datos.Repository;
using Capa_Servicio.Interface;
using Capa_Servicio.Services;
using CapaDominio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var allowedOrigin = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
var secretKey = Settings.GenerateSecretByte();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApipracticeContext>(options => options.UseSqlServer(connectionString));

// Configura CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin)
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


//authorization for all routes
builder.Services.AddAuthorization(
options =>
  {
      options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
  }
);



//builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddAutoMapper(typeof(Program));




//Aquí registro mi servicio e interfaz.
builder.Services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IHabilidades, HabilidadService>();  
builder.Services.AddScoped<IUsuario, UsuarioService>();
builder.Services.AddScoped<IProducto, ProductoService >();
builder.Services.AddScoped<IInventario, InventarioService>();
builder.Services.AddScoped<IAuditoria, AuditoriaService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddSingleton<TokenService>();






var app = builder.Build();


// Usa CORS
app.UseCors("myAppCors");



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.RegisterHabilidadEndpoints(); // <-- Add this line
app.RegisterUsuarioEndpoints(); // <-- Add this line
app.RegisterProductoEndpoints(); // <-- Add this line
app.RegisterInventarioEndpoints(); // <-- Add this line
app.RegisterAuditoriaEndpoints(); // <-- Add this line
app.RegisterLoginEndpoints(); // <-- Add this line


app.UseAuthentication();
app.UseAuthorization();
app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
