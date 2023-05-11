using blazor_todo.Shared;
using blazor_todo.Shared.Model;

namespace blazor_todo.Client.Services
{
	public interface ITodoServices
	{
		Task<List<KanbanTaskItem>> AddTask(KanbanTaskItem section);
		Task<List<KanbanTaskItem>> UpdateTask(KanbanTaskItem section);
		Task<List<KanbanTaskItem>> DeleteTask(KanbanTaskItem section);

		Task<ResponseType> AddSection(KanBanSection section);
		Task<ResponseType> UpdateSection(KanBanSection section);
		Task<ResponseType> DeleteSection(KanBanSection section);
		
		
	}
	public class TodoServices : ITodoServices
	{
		private readonly HttpClient _httpClient;
		public TodoServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<ResponseType> AddSection(KanBanSection section)
		{
			throw new NotImplementedException();
		}

		public async Task<List<KanbanTaskItem>> AddTask(KanbanTaskItem section)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseType> DeleteSection(KanBanSection section)
		{
			throw new NotImplementedException();
		}

		public async Task<List<KanbanTaskItem>> DeleteTask(KanbanTaskItem section)
		{
			throw new NotImplementedException();
		}

		public async Task<ResponseType> UpdateSection(KanBanSection section)
		{
			throw new NotImplementedException();
		}

		public async Task<List<KanbanTaskItem>> UpdateTask(KanbanTaskItem section)
		{
			throw new NotImplementedException();
		}
	}
}
