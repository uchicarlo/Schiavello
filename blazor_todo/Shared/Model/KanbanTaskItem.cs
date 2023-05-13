using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Model
{
	public class KanbanTaskItem
	{
		public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
		public string? Status { get; set; }
		public bool IsEditing { get; set; } = false;

		public KanbanTaskItem() { }
		public KanbanTaskItem(string name, string status)
		{
			Name = name;
			Status = status;
		}
	}
}
