using M2I_ExempleCours.repository;
using Microsoft.AspNetCore.Mvc;

namespace M2I_ExempleCours.Controllers;

public class StudentController(IStudentRepository repository) : Controller
{
	public IActionResult Index()
	{
		return View(repository.GetAllStudentsAsync().Result);
	}

	[HttpPost]
	public IActionResult AddStudent(string firstname, string lastname)
	{
		repository.AddUserAsync(firstname, lastname);
		return RedirectToAction("Index");
	}
}