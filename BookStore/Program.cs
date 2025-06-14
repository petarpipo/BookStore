
using BookStore;
using BookStore.Models;
using BookStore.Repositories.Implementation;
using BookStore.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookStore.Services.Implementation;
using BookStore.Services.Interfaces;
using BookStore.MapperProfiles;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


// Add services to the container.
builder.Services.AddAutoMapper(typeof(BookProfile));
builder.Services.AddAutoMapper(typeof(GenreProfile));
builder.Services.AddControllers();
builder.Services.AddDbContext<BookStoreContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=bookstoredb;Trusted_Connection=True;");
});


//Repositories

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

//Services

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrderService, OrderService>();

// For Identity
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<BookStoreContext>()
    .AddDefaultTokenProviders();

//Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(0),
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddCors(options =>
{

    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost:4200","*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
