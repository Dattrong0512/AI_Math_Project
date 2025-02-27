using AI_Math_Project.Data;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Math AI Api",
        Description = "This is all api services provide for AI Math product",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Email",
            Email = "trongleviet05@gmail.com"
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
}).AddSwaggerGenNewtonsoftSupport();




builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);


var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

//DbContext is a layer, which present for database
//DbContext need DbContextOption to configuring parameter for DbContext
//Register a services to DI DbcontextOption into a DbContext.
builder.Services.AddDbContext<ApplicationDBContext>(options =>

    options.UseSqlServer(connectionStrings, sqlServerOptions =>
    {
        sqlServerOptions.CommandTimeout(60);
        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }),
    ServiceLifetime.Scoped
);



//Register DI for Repo
builder.Services.AddScoped<IChapterRepository, ChapterRepository>();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
