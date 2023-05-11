using blazor_todo.Server.Context;
using blazor_todo.Shared.Model;

namespace blazor_todo.Server.Operations
{
	public class Query
	{
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanBanSection> GetKanBanSections(ToDoContext context) => context.KanBanSections;
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanbanTaskItem> GetKanbanTaskItems(ToDoContext context) => context.KanbanTaskItems;
	}
}
