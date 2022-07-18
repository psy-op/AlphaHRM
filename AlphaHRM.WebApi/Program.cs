
using AlphaHRM.BL;
using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var Key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = builder.Configuration["JWT:Issuer"],
		ValidAudience = builder.Configuration["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key)
	};
});

builder.Services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();




builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<EFContext>(options =>
    options.UseSqlServer(@"Data Source=DESKTOP-OROOU30;Initial Catalog=HRM;Integrated Security=True"));
builder.Services.AddTransient<IVacationManager, VacationSQLManager>();
builder.Services.AddTransient<IUserManager, UserSQLManager>();
builder.Services.AddTransient<Mapper>();
builder.Services.AddTransient<Hasher>();


//

//
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
