using ASP_Ex.Data;
using ASP_Ex.Data.DAL;
using ASP_Ex.Services.Hash;
using ASP_Ex.Services.Kdf;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHashService, ShaHashService>();

builder.Services.AddDbContext<DataContext>(
	options => options.UseSqlServer(
		builder.Configuration.GetConnectionString("LocalMSSQL")),
	ServiceLifetime.Singleton);


builder.Services.AddSingleton<DataAccessor>();
builder.Services.AddSingleton<IKdfService, Pbkdf1Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
				.AllowCredentials());

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
