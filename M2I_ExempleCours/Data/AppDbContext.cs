using M2I_ExempleCours.Models;
using Microsoft.EntityFrameworkCore;

namespace M2I_ExempleCours.Data;

public class AppDbContext : DbContext
{
	public DbSet<Student> Students { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Student>().HasData(
			new Student { Id = 1, Firstname = "Florian", Lastname = "Desmet" },
			new Student { Id = 2, Firstname = "Melany", Lastname = "Froment" },
			new Student { Id = 3, Firstname = "Dylan", Lastname = "Olivro" },
			new Student { Id = 4, Firstname = "Axel", Lastname = "Damart" },
			new Student { Id = 5, Firstname = "Jessica", Lastname = "Lejeune" },
			new Student { Id = 666, Firstname = "Gauthier", Lastname = "Auge" }
		);
	}
}