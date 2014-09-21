using System;
using System.Collections.Generic;

namespace RS.Shared.Model
{
	public class Skill
	{
		public int id { get; set; }
		public List<Level> levels { get; set; }
		public string title { get; set; }
		public string description { get; set; }
	}
}