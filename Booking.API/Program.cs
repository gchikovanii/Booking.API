using Booking.Application.Abstraction;
using Booking.Application.ConfigurationOptions;
using Booking.Application.Implementation;
using Booking.Application.Services.Abstraction.ICloudinaryService;
using Booking.Application.Services.Abstraction.IHotelService;
using Booking.Application.Services.Abstraction.JwtAuth;
using Booking.Application.Services.Abstraction.UserService;
using Booking.Application.Services.Implementation;
using Booking.Application.Services.Implementation.CloudinaryService;
using Booking.Application.Services.Implementation.HotelService;
using Booking.Application.Services.Implementation.JwtAuth;
using Booking.Domain.Entities.UserAggregate;
using Booking.Infrastructure.DataContext;
using Booking.Infrastructure.Repository.Abstraction;
using Booking.Infrastructure.Repository.Implementation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
#region Swagger and Jwt bearer
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo() { Title = "Booking.API", Version = "v1" });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Bearer Scheme"
    });
    
    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
#endregion

#region JWT
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


}).AddJwtBearer(option =>
{
    option.RequireHttpsMetadata = false;
    option.SaveToken = true;
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtToken").Value))
    };
});
builder.Services.AddSingleton<IJwtAuthetificationManager>
    (new JwtAuthetificationManager(builder.Configuration.GetSection("JwtToken").Value));

#endregion
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//For Hasing user password back
//builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);
//Cloudinary
builder.Services.Configure<CloudinarySetting>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddTransient<IHotelService, HotelService>();
builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IHotelRoomTypeNumbersRepository, HotelRoomTypeNumbersRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IOrderRepository,OrderRepository>();
builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
builder.Services.AddTransient<IHotelImageService, HotelImageService>();
builder.Services.AddTransient<IHotelImageRepository, HotelImageRepository>();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
