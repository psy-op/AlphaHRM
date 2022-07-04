using AlphaHRM.BL;
using AlphaHRM.DAL;
using AlphaHRM.Intereface;
using AlphaHRM.Utilities;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<EFContext>(options =>
    options.UseSqlServer(@"Data Source=DESKTOP-OROOU30;Initial Catalog=HRM;Integrated Security=True"));
builder.Services.AddTransient<IVacationManager, VacationSQLManager>();
builder.Services.AddTransient<IUserManager, UserSQLManager>();
builder.Services.AddTransient<Mapper>();
builder.Services.AddTransient<Hasher>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
