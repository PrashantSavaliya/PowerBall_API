using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PowerBall.Data;
using PowerBall.Repository;
using PowerBall.Repository.Interefaces;
using System.Text;
using Twilio.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
    // Add JWT authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new List<string>()
        }
    });
});


builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(OTPoptions =>
{
    OTPoptions.IdleTimeout = TimeSpan.FromMinutes(1); // Set session timeout
    OTPoptions.Cookie.HttpOnly = true;
    OTPoptions.Cookie.IsEssential = true;
});

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();

builder.Services.AddDbContext<DataContext>(item => item.UseSqlServer(config.GetConnectionString("DBCS")));

builder.Services.AddHttpClient<ITwilioRestClient, TwilioClient>();
builder.Services.AddScoped<IAuthRepo , AuthRepo>();
builder.Services.AddScoped<IGameRepo , GameRepo>();
builder.Services.AddScoped<IDrawRepo , DrawRepo>();
builder.Services.AddScoped<IResultRepo , ResultRepo>();
builder.Services.AddScoped<IDataListRepo, DataListRepo>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value == "/")
    {
        string swaggerPath = $"{context.Request.PathBase}/swagger/index.html";
        context.Response.Redirect(swaggerPath);
    }
    await next();
});

app.UseSession();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();