using Microsoft.EntityFrameworkCore;
using M2I_ExempleCours.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration de la Database 
builder.Services.AddDbContext<AppDbContext>(
	options => options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection")
	)
);

var app = builder.Build();

// Appliquer les migrations et remplir la BDD si vide
using (var scope = app.Services.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
	db.Database.Migrate();
}

// Configuration d'une route pour tester
app.MapGet("/students", async (AppDbContext db) => await db.Students.ToListAsync());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Run();