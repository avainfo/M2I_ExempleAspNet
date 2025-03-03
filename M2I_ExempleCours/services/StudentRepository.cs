using M2I_ExempleCours.Data;
using M2I_ExempleCours.Models;
using M2I_ExempleCours.repository;
using Microsoft.EntityFrameworkCore;

namespace M2I_ExempleCours.services;

public class StudentRepository(AppDbContext context) : IStudentRepository
{
	public Task<List<Student>> GetAllStudentsAsync()
	{
		return context.Students.ToListAsync();
	}

	public Task<Student?> GetStudentByIdAsync(int id)
	{
		return context.Students.FirstOrDefaultAsync(student => student.Id == id);
	}

	public Task AddUserAsync(string firstname, string lastname)
	{
		Student s = new Student()
		{
			Firstname = firstname,
			Lastname = lastname
		};
		context.Students.Add(s);
		return context.SaveChangesAsync();
	}

	public Task UpdateUserAsync(Student student)
	{
		context.Students.Update(student);
		return context.SaveChangesAsync();
	}
}