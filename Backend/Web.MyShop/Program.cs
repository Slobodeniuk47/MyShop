using Data.MyShop;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Data.MyShop.Constants;
using Infrastructure.MyShop.Mapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using Web.MyShop.Validations;
using Data.MyShop.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Infrastructure.MyShop.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Infrastructure.MyShop.Interfaces;
using Data.MyShop.Interfaces;
using Data.MyShop.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No such a connection string");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Web.MyShop")));
//options.UseNpgsql(connectionString));//PostgresSql


builder.Services.AddIdentity<UserEntity, RoleEntity>(options => 
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICommentImageRepository, CommentImageRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentImageService, CommentImageService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

//Takes away the error. One to many
// https://stackoverflow.com/questions/59199593/net-core-3-0-possible-object-cycle-was-detected-which-is-not-supported
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtSecretKey")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAutoMapper(typeof(AppMapProfile));

#pragma warning disable CS0618 //Type or member is absollute
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
#pragma warning restore CS0618 // Type or member is absolute

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
builder.Services.AddSwaggerGen(c =>
{
    var fileDoc = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");
    //c.IncludeXmlComments(fileDoc);

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "JWT Autorization using the Bearer scheme.",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"

        });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Id= "Bearer",
                     Type = ReferenceType.SecurityScheme
                 }
             },  new List<string>()
        }
    });
});
//builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(p =>
p.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

string[] directoriesToCreate = {
    DirectoriesInProject.Images,
    DirectoriesInProject.UserImages,
    DirectoriesInProject.ProductImages,
    DirectoriesInProject.CategoryImages,
    DirectoriesInProject.CommentImages,
    DirectoriesInProject.CompanyImages
};

foreach (var directoryName in directoriesToCreate)
{
    var dir = Path.Combine(Directory.GetCurrentDirectory(), directoryName);
    if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(dir),
        RequestPath = "/" + directoryName
    });
}

app.MapControllers();
app.SeedData();
Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("Started"); Console.ForegroundColor = ConsoleColor.Red; Console.Write(": by Slobodeniuk Artem\n\n"); Console.ResetColor();
app.Run();
