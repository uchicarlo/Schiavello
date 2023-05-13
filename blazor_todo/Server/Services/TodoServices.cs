using blazor_todo.Shared;
using blazor_todo.Shared.Interface;
using blazor_todo.Shared.Model;
using Dapper;
using Microsoft.Data.Sqlite;

namespace blazor_todo.Server.Services
{
	public class TodoServices : ITodoServices
	{
		private string _DbConnectionString = "Data Source=DB\\todo.db";

		public async Task<KanbanRecords> AddSection(KanBanSection section)
		{
			var result = new KanbanRecords(new(), new());
			try
			{
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "insert into kanban_sections(Name) " +
							"values(@Name)";
				await sqlConnection.ExecuteAsync(query,section);
				result = new(GetKanBanSections(), GetKanbanTaskItems());
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
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "insert into kanban_task_items(Name,Status) " +
							"values(@Name,@Status)";
				await sqlConnection.ExecuteAsync(query,item);
				result = GetKanbanTaskItems();
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
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "select * from kanban_sections where Id=@Id";
				section = sqlConnection.QueryFirstOrDefault<KanBanSection>(query,section);
				if (section == null) return new(GetKanBanSections(), GetKanbanTaskItems());
				query = "delete from kanban_sections where Id=@Id";
				await sqlConnection.ExecuteAsync(query, section);
				query = "delete from kanban_task_items where Status=@Name";
				await sqlConnection.ExecuteAsync(query, section);
				result = new(GetKanBanSections(), GetKanbanTaskItems());
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
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "delete from kanban_task_items where Id=@Id";
				await sqlConnection.ExecuteAsync(query, item);
				result = GetKanbanTaskItems();
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}

		public Task<ResponseType> GetKanban()
		{
			throw new NotImplementedException();
		}

		public async Task<KanbanRecords> UpdateSection(KanBanSection section)
		{
			var result = new KanbanRecords(new(), new());
			try
			{
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "update kanban_sections set Name=@Name where Id=@Id";
				await sqlConnection.ExecuteAsync(query, section);
				result = new(GetKanBanSections(), GetKanbanTaskItems());
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
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "update kanban_task_items set Name=@Name,Status=@Status where Id=@Id";
				await sqlConnection.ExecuteAsync(query, item);
				result = GetKanbanTaskItems();
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}

		public List<KanbanTaskItem> GetKanbanTaskItems()
		{
			var result = new List<KanbanTaskItem>();
			try
			{
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "select * from kanban_task_items";
				result = sqlConnection.Query<KanbanTaskItem>(query).ToList();

			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
		public List<KanBanSection> GetKanBanSections()
		{
			var result = new List<KanBanSection>();
			try
			{
				using var sqlConnection = new SqliteConnection(_DbConnectionString);
				var query = "select * from kanban_sections";
				result = sqlConnection.Query<KanBanSection>(query).ToList();

			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}
	}
}
