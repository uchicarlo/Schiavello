using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanbanTaskItem
	{
		public int Id { get; set; }	
		public string? Name { get; set; }
		public string? Status { get; set; }

		public KanbanTaskItem() { }
		public KanbanTaskItem(string name, string status)
		{
			Name = name;
			Status = status;
		}
	}
}
