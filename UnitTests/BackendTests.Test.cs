using M2I_ExempleCours.Controllers;
using M2I_ExempleCours.Data;
using M2I_ExempleCours.Models;
using M2I_ExempleCours.repository;
using M2I_ExempleCours.services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace UnitTests;

public class BackendTests
{
	private readonly List<Student> _initialStudents =
	[
		new() { Id = 1, Firstname = "Florian", Lastname = "Desmet" },
		new() { Id = 2, Firstname = "Melany", Lastname = "Froment" },
		new() { Id = 3, Firstname = "Dylan", Lastname = "Olivro" },
		new() { Id = 4, Firstname = "Axel", Lastname = "Damart" },
		new() { Id = 5, Firstname = "Jessica", Lastname = "Lejeune" },
		new() { Id = 666, Firstname = "Gauthier", Lastname = "Auge" }
	];

	private List<Student> _testStudents = new();

	public async Task<AppDbContext> GetDatabaseContext()
	{
		_testStudents = _initialStudents;
		var options = new DbContextOptionsBuilder<AppDbContext>()
			.UseInMemoryDatabase(databaseName: "TestDb")
			.Options;

		var databaseContext = new AppDbContext(options);
		databaseContext.Database.EnsureCreated();

		if (!await databaseContext.Students.AnyAsync())
		{
			databaseContext.Students.FromSqlRaw("DELETE from Testdb");
			databaseContext.Students.AddRange(_testStudents);
			await databaseContext.SaveChangesAsync();
		}

		return databaseContext;
	}

	[Fact]
	public async Task GetAllStudents()
	{
		var dbContext = await GetDatabaseContext();
		var studentRepo = new StudentService(dbContext);

		var students = await studentRepo.GetAllStudentsAsync();

		Assert.NotNull(students);
		Assert.Equal(_initialStudents.Count, students.Count);
	}

	[Fact]
	public async Task AddStudent()
	{
		var dbContext = await GetDatabaseContext();
		var studentRepo = new StudentService(dbContext);

		await studentRepo.AddUserAsync("Franck", "Roy");
		var students = await studentRepo.GetAllStudentsAsync();

		Assert.Equal((_initialStudents.Count + 1), students.Count);
	}


	/**
	 * CONTROLLERS
	 */
	[Fact]
	public void TestStudentControllerIndex()
	{
		Mock<IStudentRepository> repo = new Mock<IStudentRepository>();

		repo.Setup(r => r.GetAllStudentsAsync()).ReturnsAsync(_initialStudents);

		StudentController sc = new StudentController(repo.Object);

		Assert.IsType<ViewResult>(sc.Index());
		Assert.IsType<RedirectToActionResult>(sc.AddStudent("Franck", "Roy"));
	}

	[Fact]
	public void TestStudentControllerAddStudent()
	{
		Mock<IStudentRepository> repo = new Mock<IStudentRepository>();

		repo.Setup(r => r.GetAllStudentsAsync()).ReturnsAsync(_initialStudents);

		StudentController sc = new StudentController(repo.Object);

		Assert.IsType<RedirectToActionResult>(sc.AddStudent("Franck", "Roy"));
	}
}