using AI_Math_Project.Data;
using AI_Math_Project.Interfaces;
using AI_Math_Project.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

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


    //var jwtSecurityScheme = new OpenApiSecurityScheme
    //{
    //    Name = "JWT Authentication",
    //    Description = "Enter your JWT token in this field",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "bearer",
    //    BearerFormat = "JWT"
    //};
    //options.AddSecurityDefinition("Bearer", jwtSecurityScheme);


    //var securityRequirement = new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference{
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        new string []{}
    //    }
    //};

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
builder.Services.AddScoped<ILessionProgressRepository, LessionProgressRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();


////Setup JWT
//builder.Services
//    .AddAuthentication(x =>
//    {
//        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    })
//    .AddJwtBearer(x =>
//    {
//        x.SaveToken = true;
//        x.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = false,
//            ValidateAudience = false,
//            ValidateLifetime = false,
//            ValidIssuer = builder.Configuration["JWT:Issuer"],
//            ValidAudience = builder.Configuration["JWT:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"] ?? "DefaultSecretKey123"))
//        };
//    });


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(
//        "AdminOnly",
//        policyBuilder => policyBuilder.RequireAssertion(
//            context => context.User.HasClaim(claim => claim.Type == "Role")
//            && context.User.FindFirst(claim => claim.Type == "Role").Value == "1"));

//    options.AddPolicy(
//        "StaffOnly",
//               policyBuilder => policyBuilder.RequireAssertion(
//                              context => context.User.HasClaim(claim => claim.Type == "Role")
//                                         && context.User.FindFirst(claim => claim.Type == "Role").Value == "2"));
//    options.AddPolicy(
//        "AdminOrStaff",
//        policyBuilder => policyBuilder.RequireAssertion(
//                       context => context.User.HasClaim(claim => claim.Type == "Role")
//                                  && (context.User.FindFirst(claim => claim.Type == "Role").Value == "1"
//                                             || context.User.FindFirst(claim => claim.Type == "Role").Value == "2")));
//});






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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
