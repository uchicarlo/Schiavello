using blazor_todo.Shared.Interface;
using blazor_todo.Shared.Model;

namespace blazor_todo.Server.Operations
{
    public class Query
	{
		private readonly ITodoServices _todoServices;
		public Query(ITodoServices todoServices)
		{
			_todoServices = todoServices;
		}	
		[GraphQLName("kanbanSections")]
		public List<KanBanSection> GetKanBanSections() => _todoServices.GetKanBanSections();

		[GraphQLName("kanbanTaskItems")]
		public List<KanbanTaskItem> GetKanbanTaskItems() => _todoServices.GetKanbanTaskItems();

	}
}
