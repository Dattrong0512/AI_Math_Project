using AI_Math_Project.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);


var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");

//DbContext is a layer, which present for database
//DbContext need DbContextOption to configuring parameter for DbContext
//Register a services to DI DbcontextOption into a DbContext.
builder.Services.AddDbContext<AiMathContext>(options =>

    options.UseSqlServer(connectionStrings, sqlServerOptions =>
    {
        sqlServerOptions.CommandTimeout(60);
        sqlServerOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    }),
    ServiceLifetime.Scoped
    //// Inject LoggerFactory vào DbContext để hỗ trợ `ILogger<T>` ghi log trong trường hợp truy vấn
    //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
    //options.UseLoggerFactory(loggerFactory);
);


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AiMathContext>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
