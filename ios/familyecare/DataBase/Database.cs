using System;
using System.Collections.Generic;
using System.Linq;
using Path = System.IO.Path;

using SQLite;
using System.Globalization;


namespace familyecare
{
	public class Database : SQLiteConnection
	{
		public Database (string path) : base(path)
		{
			CreateTable<Event>();
			//CreateTable<Message>();
		}

		public Event AddEvent(Event e)
		{
			Insert(e);
			return e;
		}


	}
}

