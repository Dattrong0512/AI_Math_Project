using AIMathProject.API.Handler;
using AIMathProject.API.Middleware;
using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Command.Register;
using AIMathProject.Application.Dto;
using AIMathProject.Application.Dto.EnrollmentDto;
using AIMathProject.Application.Dto.LessonProgressDto;
using AIMathProject.Application.Dto.QuestionDto;
using AIMathProject.Application.Mappers;
using AIMathProject.Domain.Entities;
using AIMathProject.Domain.Interfaces;
using AIMathProject.Infrastructure.CommonServices;
using AIMathProject.Infrastructure.Data;
using AIMathProject.Infrastructure.Options;
using AIMathProject.Infrastructure.Processors;
using AIMathProject.Infrastructure.Repositories;
using AIMathProject.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterCommand).Assembly));
builder.Services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Math AI Api",
        Description = @"
            Test Login Accounts:
            To test the login functionality, you can use the following test accounts:

            Admin Account:
            - **Email**: admin@example.com
            - **Password**: Admin@123

            User Account:
            - **Email**: michael.brown@example.com
            - **Password**: Michael@101

            Use these credentials to log in and explore the API endpoints.
        ",
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


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString, option =>
    {
        option.CommandTimeout(60);
        option.EnableRetryOnFailure(10, TimeSpan.FromSeconds(60), null);
    });
});

builder.Services.AddHttpContextAccessor();


builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection(JwtOptions.JwtOptionKey));

builder.Services.AddScoped<IAuthTokenProcessor, AuthTokenProcessor>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChapterRepository<ChapterDto>, ChapterRepository>();
builder.Services.AddScoped<IEnrollmentRepository<EnrollmentDto>, EnrollmentRepository>();
builder.Services.AddScoped<ILessonRepository<LessonDto>, LessonRepository>();
builder.Services.AddScoped<ILessonProgressRepository<LessonProgressDto>, LessonProgressRepository>();
builder.Services.AddScoped<IQuestionRepository<QuestionDto>, QuestionRepository>();


// Đăng ký CustomAuthenticationSchemeProvider
builder.Services.AddSingleton<IAuthenticationSchemeProvider, CustomAuthenticationSchemeProvider>();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie()
.AddGoogle(options =>
{
    var clientId = builder.Configuration["Authentication:Google:ClientID"];
   
    if (clientId == null)
    {
        throw new ArgumentNullException(nameof(clientId));
    }
    var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    if (clientSecret == null)
    {
        throw new ArgumentNullException(nameof(clientSecret));
    }
    options.ClientId = clientId;
    options.ClientSecret = clientSecret;
    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddJwtBearer("Bearer", options =>
{
    var jwtOptions = builder.Configuration.GetSection(JwtOptions.JwtOptionKey)
        .Get<JwtOptions>() ?? throw new ArgumentException(nameof(JwtOptions));
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["ACCESS_TOKEN"];
            return Task.CompletedTask;
        }
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("User", policy => policy.RequireClaim("Role", "User"));
    options.AddPolicy("Admin", policy => policy.RequireClaim("Role", "Admin"));
    options.AddPolicy("UserOrAdmin", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(c => c.Type == "Role" && (c.Value == "User" || c.Value == "Admin"))));
});
builder.Services.AddIdentity<User, IdentityRole<int>>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequiredLength = 8;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false; // Yêu cầu ít nhất 1 ký tự đặc biệt
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ " + // Ký tự Latin và một số ký tự đặc biệt
        "áàảãạâấầẩẫậăắằẳẵặéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđ" + // Ký tự tiếng Việt
        "ÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ"; // Chữ cái in hoa tiếng Việt
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // Đăng ký GlobalExceptionHandler
//builder.Services.AddScoped<UserSeeder>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(ops => ops.TokenLifespan=TimeSpan.FromHours(10));

var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailHelper, EmailSender>();
builder.Services.AddScoped<IEmailTemplateReader, EmailTemplateReader>();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseStaticFiles();
app.UseCors(MyAllowSpecificOrigins);
app.UseMiddleware<RefreshTokenMiddleware>();
app.UseExceptionHandler(_ => { });


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();




//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {

//        // Chạy UserSeeder
//        var userSeeder = services.GetRequiredService<UserSeeder>();
//        // Đường dẫn đến file SeedUsersFromJson.json
//        var currentPath = Directory.GetCurrentDirectory(); // Ví dụ: C:\Users\Hi\Desktop\AI_MATH\AIMathProject\AIMathProject.API
//        var correctedPath = currentPath.Replace("AIMathProject.API", ""); // Loại bỏ AIMathProject.API
//        var jsonFilePathUser = Path.Combine(correctedPath, "AIMathProject.Domain", "DataSeeding", "SeedUsersFromJson.json");
//        await userSeeder.SeedUsersFromJson(jsonFilePathUser, "User");
//        var jsonFilePathAdmin = Path.Combine(correctedPath, "AIMathProject.Domain", "DataSeeding", "SeedAdminFromJson.json");
//        await userSeeder.SeedUsersFromJson(jsonFilePathAdmin, "Admin");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Lỗi khi chạy seeding: {ex.Message}");
//    }
//}
