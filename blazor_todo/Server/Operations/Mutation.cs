using blazor_todo.Server.Context;
using blazor_todo.Shared.Model;
using Microsoft.EntityFrameworkCore;
namespace blazor_todo.Server.Operations
{
	public class Mutation
	{
		public List<KanbanTaskItem> AddTaskItem([Service] ToDoContext _toDoContext,string name,string status)
		{
			var item = new KanbanTaskItem(name,status);
			_toDoContext.Entry(item).State = EntityState.Added;
			//_toDoContext.KanbanTaskItems.Add(item);
			_toDoContext.SaveChanges();
			return _toDoContext.KanbanTaskItems.ToList();
		}
		public List<KanbanTaskItem> UpdateTaskItem([Service] ToDoContext _toDoContext, int id,string name,string status)
		{
			var item = _toDoContext.KanbanTaskItems.FirstOrDefault(x => x.Id == id);
			if (item == null)
				return _toDoContext.KanbanTaskItems.ToList();
			item.Status = status;
			item.Name = name;
			_toDoContext.Entry(item).State = EntityState.Modified;
			//_toDoContext.KanbanTaskItems.Update(item);
			_toDoContext.SaveChanges();
			return _toDoContext.KanbanTaskItems.ToList();
		}
		public List<KanbanTaskItem> DeleteTaskItem([Service] ToDoContext _toDoContext, int id)
		{
			var item = _toDoContext.KanbanTaskItems.FirstOrDefault(x => x.Id == id);
			if (item == null)
				return _toDoContext.KanbanTaskItems.ToList();
			_toDoContext.Entry(item).State = EntityState.Deleted;
			_toDoContext.SaveChanges();
			return _toDoContext.KanbanTaskItems.ToList();
		}
		public KanbanRecords AddSections([Service] ToDoContext _toDoContext, string name)
		{
			if (_toDoContext.KanBanSections.FirstOrDefault(x => x.Name == name) != null)
				return new(_toDoContext.KanBanSections.ToList(), _toDoContext.KanbanTaskItems.ToList());
			var section = new KanBanSection(name, false, string.Empty);
			_toDoContext.Entry(section).State = EntityState.Added;
			_toDoContext.SaveChanges();
			return new(_toDoContext.KanBanSections.ToList(),_toDoContext.KanbanTaskItems.ToList());
		}
		public KanbanRecords RenameSections([Service] ToDoContext _toDoContext, int id,string name)
		{
			var section = _toDoContext.KanBanSections.FirstOrDefault(x => x.Id == id);
			if (section == null)
				return new(_toDoContext.KanBanSections.ToList(), _toDoContext.KanbanTaskItems.ToList());
			section.Name = name;
			_toDoContext.Entry(section).State = EntityState.Modified;
			_toDoContext.SaveChanges();
			return new(_toDoContext.KanBanSections.ToList(), _toDoContext.KanbanTaskItems.ToList());
		}

		public KanbanRecords DeleteSections([Service] ToDoContext _toDoContext, int id)
		{
			var section = _toDoContext.KanBanSections.FirstOrDefault(x => x.Id == id);
			if (section == null)
				return new(_toDoContext.KanBanSections.ToList(), _toDoContext.KanbanTaskItems.ToList());
			_toDoContext.Entry(section).State = EntityState.Deleted;
			var items = _toDoContext.KanbanTaskItems.Where(x => x.Status == section.Name);
			_toDoContext.Entry(items).State = EntityState.Deleted;
			_toDoContext.SaveChanges();
			return new(_toDoContext.KanBanSections.ToList(), _toDoContext.KanbanTaskItems.ToList());
		}
	}
}
