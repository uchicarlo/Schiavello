using blazor_todo.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blazor_todo.Shared.Interface
{
	public interface ITodoServices
	{
		List<KanBanSection> GetKanBanSections();
		List<KanbanTaskItem> GetKanbanTaskItems();
		Task<ResponseType> GetKanban();
		Task<List<KanbanTaskItem>> AddTask(KanbanTaskItem item);
		Task<List<KanbanTaskItem>> UpdateTask(KanbanTaskItem item);
		Task<List<KanbanTaskItem>> DeleteTask(KanbanTaskItem item);

		Task<KanbanRecords> AddSection(KanBanSection section);
		Task<KanbanRecords> UpdateSection(KanBanSection section);
		Task<KanbanRecords> DeleteSection(KanBanSection section);


	}
}
