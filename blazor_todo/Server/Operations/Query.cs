using blazor_todo.Server.Context;
using blazor_todo.Shared.Model;

namespace blazor_todo.Server.Operations
{
	public class Query
	{
		[GraphQLName("kanbanSections")]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanBanSection> GetKanBanSections(ToDoContext context) => context.KanBanSections;

		[GraphQLName("kanbanTaskItems")]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanbanTaskItem> GetKanbanTaskItems(ToDoContext context) => context.KanbanTaskItems;
	}
}
