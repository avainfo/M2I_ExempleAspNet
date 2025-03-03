using M2I_ExempleCours.Models;

namespace M2I_ExempleCours.repository;

public interface IStudentRepository
{
	Task<List<Student>> GetAllStudentsAsync();
	Task<Student?> GetStudentByIdAsync(int id);
	Task AddUserAsync(string firstname, string lastname);
	Task UpdateUserAsync(Student student);
}