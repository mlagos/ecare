using System;
using SQLite;
using System.Linq;

namespace familyecare
{
	/// <summary>
	/// Evento.
	/// </summary>
	public class Event
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		[Indexed]
		public DateTime EventDateTime { get; set; }
		[MaxLength(148)]
		public string Description { get; set; }
		public bool Checked { get; set; }
	}
}

