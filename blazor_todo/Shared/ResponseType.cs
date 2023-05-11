using blazor_todo.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared
{
	public class ResponseType
	{
		public List<KanBanSection> kanbanSections { get; set; } = new();
		public List<KanbanTaskItem> kanbanTaskItems { get; set; } = new();
		public KanbanRecords kanbanRecords { get; set; } = new(new(),new());

	}
}
