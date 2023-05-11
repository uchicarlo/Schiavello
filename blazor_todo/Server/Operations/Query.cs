using blazor_todo.Server.Context;
using blazor_todo.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace blazor_todo.Server.Operations
{
	public class Query
	{
		[GraphQLName("kanbanSections")]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanBanSection> GetKanBanSections([Service] ToDoContext _toDoContext) => _toDoContext.KanBanSections;

		[GraphQLName("kanbanTaskItems")]
		[UseProjection]
		[UseFiltering]
		[UseSorting]
		public IQueryable<KanbanTaskItem> GetKanbanTaskItems([Service] ToDoContext _toDoContext) => _toDoContext.KanbanTaskItems;

	}
}
