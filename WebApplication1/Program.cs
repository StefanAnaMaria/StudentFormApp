using Microsoft.EntityFrameworkCore;
using WebApplication1.Repos;
using Microsoft.EntityFrameworkCore.SqlServer;
using WebApplication1.Services;
namespace WebApplication1;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

public class Program
{
    public static void Main(string[] args)
    {
        
        var builder = WebApplication.CreateBuilder(args);
        // builder.Services.AddCors(options =>
        // {
        //     options.AddPolicy("AllowLocalhost3000",
        //         builder => builder.WithOrigins("http://localhost:3000")  // Permite cereri doar de la acest URL
        //                           .AllowAnyMethod()  // Permite orice metodă HTTP (GET, POST, etc.)
        //                           .AllowAnyHeader());  // Permite orice header
        // });


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin() // Permite orice origine (pentru dezvoltare, dar nu recomandat în producție)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        // Adăugarea serviciilor pentru controllerele API
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        // Apoi în `app.UseCors(...)`
       

           //adaugare controllers
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            }
        );
        
        //servicii
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IStudentFormService, StudentFormService>();
  
        // Adăugarea serviciilor pentru Swagger
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();



        // 🛡️ Configurarea autentificării pe baza de JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer",options =>
            {
                var key = builder.Configuration["Jwt:Key"];
                var issuer = builder.Configuration["Jwt:Issuer"];
                var audience = builder.Configuration["Jwt:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key))
                };
            });


        // 💼 Configurarea autorizației
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            options.AddPolicy("User", policy => policy.RequireRole("User"));
        });

        // 🧱 1. Conectarea la baza de date
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

 

        // implmnetare cu sql
        builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
        builder.Services.AddScoped<IRoleRepository, SqlRoleRepository>();
        // builder.Services.AddScoped<IStudentFormRepository, SqlStudentFormRepository>();
        builder.Services.AddScoped<IStudentFormRepository, SqlStudentFormRepository>();

        builder.Services.AddSingleton<JwtService>();
        builder.Services.AddSingleton<PdfGenerationService>();


        string pdfDirectory = Path.Combine(builder.Environment.ContentRootPath, "pdfs");

        // Crează directorul dacă nu există deja
        if (!Directory.Exists(pdfDirectory))
        {
            Directory.CreateDirectory(pdfDirectory);
        }
        var app = builder.Build();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "pdfs")),
            RequestPath = "/pdfs"
        });


        // 🛡️ Middleware-uri
    if (app.Environment.IsDevelopment())
        {
        app.UseSwagger();
        app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();
        app.UseAuthentication(); // Activăm autentificarea
        app.UseAuthorization(); // Activăm autorizația
        app.UseCors("AllowAll"); // Activăm politica CORS pentru localhost:3000
        app.MapControllers();

        app.Run();
    }
}
