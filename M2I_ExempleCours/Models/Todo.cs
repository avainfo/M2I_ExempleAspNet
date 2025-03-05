namespace M2I_ExempleCours.Models;

public class Todo
{
	public required int Id { get; set; }
	public required string Title { get; set; }
	public bool Completed { get; set; }

	public override bool Equals(object? obj)
	{
		if (obj.GetType() == typeof(Todo))
		{
			Todo objTodo = (Todo)obj;
			return objTodo.Id == Id;
		}

		return false;
	}
}