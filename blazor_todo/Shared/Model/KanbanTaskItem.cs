using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanbanTaskItem
	{
		public string Name { get; init; }
		public string Status { get; set; }

		public KanbanTaskItem(string name, string status)
		{
			Name = name;
			Status = status;
		}
	}
}
