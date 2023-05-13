using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanBanSection
	{
		public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
		public bool NewTaskOpen { get; set; } = false;
		public string? NewTaskName { get; set; }
		public bool IsEditing { get; set; } = false;

		public KanBanSection() { }
		public KanBanSection(string name, bool newTaskOpen, string newTaskName)
		{
			Name = name;
			NewTaskOpen = newTaskOpen;
			NewTaskName = newTaskName;
		}
	}

	public record KanbanRecords(List<KanBanSection> kanbanSections, List<KanbanTaskItem> kanbanTaskItems);
}
