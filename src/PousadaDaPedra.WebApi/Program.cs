using Microsoft.EntityFrameworkCore;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Application.UseCases.UsuerUseCase;
using PousadaDaPedra.Infrastructure.Data.Context;
using PousadaDaPedra.Infrastructure.Data.Repositories;
using PousadaDaPedra.Infrastructure.Data.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PousadaDaPedra.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString, b =>
        b.MigrationsAssembly("PousadaDaPedra.Infrastructure")));

// Repositories
builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// UseCases de Tarefa
builder.Services.AddScoped<CriarTarefa>();
builder.Services.AddScoped<FinalizarTarefa>();
builder.Services.AddScoped<IniciarTarefa>();
builder.Services.AddScoped<ListarTarefasUseCase>();
builder.Services.AddScoped<AtualizarTarefaUseCase>();
builder.Services.AddScoped<ReabrirUseCase>();
builder.Services.AddScoped<ListarUserUseCase>();

// Token JWT
builder.Services.AddScoped<ITokenService, JwtTokenService>();

//UseCases de Usuarios
builder.Services.AddScoped<CriarUserUseCase>();
builder.Services.AddScoped<LoginUserUseCase>();



var key = Encoding.UTF8.GetBytes(
    builder.Configuration["Jwt:Key"]!
);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(key)
            };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token =
                    context.Request.Cookies["auth_token"];

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();



// --- ADICIONE ESTE BLOCO AQUI ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
    "http://localhost:3000",
    "http://localhost:5173"
) // URL exata do seu Next.js
            .AllowAnyHeader()                     // Permite Content-Type, Authorization, etc.
            .AllowAnyMethod()                     // Permite GET, POST, PUT, DELETE
            .AllowCredentials();                  // Importante! Permite que o fetch envie os Cookies
    });
});
// --------------------------------

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// --- ADICIONE ESTA LINHA AQUI ---
app.UseCors("AllowFrontend");
// --------------------------------

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();