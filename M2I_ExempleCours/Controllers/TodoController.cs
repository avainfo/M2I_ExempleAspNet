using M2I_ExempleCours.Models;
using Microsoft.AspNetCore.Mvc;

namespace M2I_ExempleCours.Controllers;

public class TodoController : Controller
{
	private static List<Todo> _todos = [];

	// GET
	public IActionResult Index()
	{
		return View(_todos);
	}

	[HttpPost]
	public IActionResult AddTask(string title)
	{
		if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
		{
			Todo task = new Todo { Id = _todos.Count + 1, Title = title };
			_todos.Add(task);
		}

		return RedirectToAction("Index");
	}

	[HttpPost]
	public IActionResult CompleteTask(int id)
	{
		Todo? task = _todos.FirstOrDefault(todo => todo.Id == id);
		if (task != null)
		{
			task.Completed = true;
		}

		return RedirectToAction("Index");
	}
}