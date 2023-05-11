using blazor_todo.Client.Utility;
using blazor_todo.Shared;
using blazor_todo.Shared.Model;

namespace blazor_todo.Client.Services
{
	public interface ITodoServices
	{
		Task<ResponseType> GetKanban();
		Task<List<KanbanTaskItem>> AddTask(KanbanTaskItem item);
		Task<List<KanbanTaskItem>> UpdateTask(KanbanTaskItem item);
		Task<List<KanbanTaskItem>> DeleteTask(KanbanTaskItem item);

		Task<KanbanRecords> AddSection(KanBanSection section);
		Task<KanbanRecords> UpdateSection(KanBanSection section);
		Task<KanbanRecords> DeleteSection(KanBanSection section);


	}
	public class TodoServices : ITodoServices
	{
		private readonly HttpClient _httpClient;
		public TodoServices(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<ResponseType> GetKanban()
		{
			var result = new ResponseType();
			try
			{
				var query = @"query{
							  kanbanSections{
								id
								name
								newTaskOpen
								newTaskName
							  }
							 kanbanTaskItems{
							  id
							  name
							  status
							 }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query);
				if (errors == null)
				{
					result = response;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
		public async Task<KanbanRecords> AddSection(KanBanSection section)
		{
			var result = new KanbanRecords(new(), new());
			try
			{
				var query = @"mutation($name: String!){
							  kanbanRecords:addSections(name: $name){
								kanbanSections{
								id
								name
								newTaskOpen
								newTaskName
							  }
							 kanbanTaskItems{
							  id
							  name
							  status
							 }
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { name = section.Name });
				if (errors == null)
				{
					result = response.kanbanRecords;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
		public async Task<KanbanRecords> UpdateSection(KanBanSection section)
		{
			var result = new KanbanRecords(new(), new());
			try
			{
				var query = @"mutation($name: String!,$id: Int!){
							  kanbanRecords:renameSections(name: $name,id: $id){
								kanbanSections{
								id
								name
								newTaskOpen
								newTaskName
							  }
							 kanbanTaskItems{
							  id
							  name
							  status
							 }
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { name = section.Name,id = section.Id });
				if (errors == null)
				{
					result = response.kanbanRecords;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}

		public async Task<KanbanRecords> DeleteSection(KanBanSection section)
		{
			var result = new KanbanRecords(new(), new());
			try
			{
				var query = @"mutation($name: String!){
							  kanbanRecords:deleteSections(name: $name){
								kanbanSections{
								id
								name
								newTaskOpen
								newTaskName
							  }
							 kanbanTaskItems{
							  id
							  name
							  status
							 }
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { name = section.Name });
				if (errors == null)
				{
					result = response.kanbanRecords;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}

		public async Task<List<KanbanTaskItem>> AddTask(KanbanTaskItem item)
		{
			var result = new List<KanbanTaskItem>();
			try
			{
				var query = @"mutation($name: String!,$status: String!){
							  kanbanTaskItems:addTaskItem(name: $name,status: $status){
								id
								name
								status
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { name = item.Name,status = item.Status });
				if (errors == null)
				{
					result = response.kanbanTaskItems;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
		public async Task<List<KanbanTaskItem>> DeleteTask(KanbanTaskItem item)
		{
			var result = new List<KanbanTaskItem>();
			try
			{
				var query = @"mutation($id: Int!){
							  kanbanTaskItems:deleteTaskItem(id: $id){
								id
								name
								status
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { id = item.Id });
				if (errors == null)
				{
					result = response.kanbanTaskItems;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
		public async Task<List<KanbanTaskItem>> UpdateTask(KanbanTaskItem item)
		{
			var result = new List<KanbanTaskItem>();
			try
			{
				var query = @"mutation($name: String!,$status: String!,$id: Int!){
							  kanbanTaskItems:updateTaskItem(name: $name,status: $status,id: $id){
								id
								name
								status
							  }
							}";
				(var response, var errors) = await _httpClient.Query<ResponseType>(query, new { name = item.Name, status = item.Status, id = item.Id });
				if (errors == null)
				{
					result = response.kanbanTaskItems;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
	}
}
