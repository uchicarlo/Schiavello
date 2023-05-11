using blazor_todo.Server.Context;
using blazor_todo.Shared.Model;

namespace blazor_todo.Server.Operations
{
	public class Mutation
	{
		public List<KanbanTaskItem> AddTaskItem(ToDoContext context, KanbanTaskItem item)
		{
			if (context.KanbanTaskItems.FirstOrDefault(x => x == item) != null) 
				return context.KanbanTaskItems.Where(x => x.Status == item.Status).ToList();
			context.KanbanTaskItems.Add(item);
			context.SaveChanges();
			return context.KanbanTaskItems.Where(x => x.Status == item.Status).ToList();
		}
		public List<KanbanTaskItem> UpdateTaskItem(ToDoContext context, KanbanTaskItem item)
		{
			context.KanbanTaskItems.Update(item);
			context.SaveChanges();
			return context.KanbanTaskItems.Where(x => x.Status == item.Status).ToList();
		}
		public List<KanbanTaskItem> DeleteTaskItem(ToDoContext context, KanbanTaskItem item)
		{
			context.KanbanTaskItems.Remove(item);
			context.SaveChanges();
			return context.KanbanTaskItems.Where(x => x.Status == item.Status).ToList();
		}
		public List<KanBanSection> AddSections(ToDoContext context,KanBanSection sections)
		{
			if (context.KanBanSections.FirstOrDefault(x => x.Name == sections.Name) != null)
				return context.KanBanSections.ToList();
			context.KanBanSections.Add(sections);
			context.SaveChanges();
			return context.KanBanSections.ToList();
		}
		public List<KanBanSection> RenameSections(ToDoContext context, KanBanSection sections)
		{
			if (context.KanBanSections.FirstOrDefault(x => x.Id == sections.Id) == null)
				return context.KanBanSections.ToList();
			context.KanBanSections.Update(sections);
			context.SaveChanges();
			return context.KanBanSections.ToList();
		}

		public List<KanBanSection> DeleteSections(ToDoContext context, KanBanSection sections)
		{
			context.KanBanSections.Remove(sections);
			context.SaveChanges();
			return context.KanBanSections.ToList();
		}
	}
}
