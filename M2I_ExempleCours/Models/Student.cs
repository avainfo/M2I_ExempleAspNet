using System.ComponentModel.DataAnnotations;

namespace M2I_ExempleCours.Models;

public class Student
{
	public int Id { get; set; }
	[MaxLength(50)]
	public string Firstname {get; set;} = string.Empty;
	[MaxLength(50)]
	public string Lastname {get; set;} = string.Empty;
}