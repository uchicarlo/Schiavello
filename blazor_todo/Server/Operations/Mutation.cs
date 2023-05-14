using blazor_todo.Shared.Interface;
using blazor_todo.Shared.Model;
namespace blazor_todo.Server.Operations
{
    public class Mutation
	{
		private readonly ITodoServices _todoServices;
		public Mutation(ITodoServices todoServices)
		{
			_todoServices = todoServices;
		}
		public async Task<List<KanbanTaskItem>> AddTaskItem(string name,string status)
		{
			var item = new KanbanTaskItem(name,status);
			return await _todoServices.AddTask(item);
		}
		public async Task<List<KanbanTaskItem>> UpdateTaskItem(int id,string name,string status)
		{
			var task = new KanbanTaskItem(name, status)
			{
				Id = id
			};
			return await _todoServices.UpdateTask(task);
		}
		public async Task<List<KanbanTaskItem>> DeleteTaskItem(int id)
		{
			var task = new KanbanTaskItem
			{
				Id = id
			};
			return await _todoServices.DeleteTask(task);
		}
		public async Task<KanbanRecords> AddSections(string name)
		{
			var section = new KanBanSection(name, false, string.Empty);
			return await _todoServices.AddSection(section);
		}
		public async Task<KanbanRecords> RenameSections(int id,string name)
		{
			var section = new KanBanSection(name, false, string.Empty)
			{
				Id = id
			};
			return await _todoServices.UpdateSection(section);
		}

		public async Task<KanbanRecords> DeleteSections(int id)
		{
			var section = new KanBanSection()
			{
				Id = id
			};
			return await _todoServices.DeleteSection(section);
		}
	}
}
