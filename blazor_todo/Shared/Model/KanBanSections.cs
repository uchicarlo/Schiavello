using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanBanSection
	{
		public int Id { get; set; }
		public string Name { get; init; }
		public bool NewTaskOpen { get; set; }
		public string NewTaskName { get; set; }

		public KanBanSection(string name, bool newTaskOpen, string newTaskName)
		{
			Name = name;
			NewTaskOpen = newTaskOpen;
			NewTaskName = newTaskName;
		}
	}
}
