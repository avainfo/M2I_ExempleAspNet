namespace M2I_ExempleCours.Models;

public class Todo
{
	public required int Id { get; set; }
	public required string Title { get; set; }
	public bool Completed { get; set; }
}