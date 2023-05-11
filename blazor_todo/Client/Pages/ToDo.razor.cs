using blazor_todo.Shared.Model;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;

namespace blazor_todo.Client.Pages
{
	public partial class ToDo
	{
		private MudDropContainer<KanbanTaskItem> _dropContainer;

		private bool _addSectionOpen;
		/* handling board events */
		private void TaskUpdated(MudItemDropInfo<KanbanTaskItem> info)
		{
			info.Item.Status = info.DropzoneIdentifier;
		}

		/* Setup for board  */
		private List<KanBanSection> _sections = new()
		{
			new KanBanSection("To Do", false, String.Empty),
			new KanBanSection("In Process", false, String.Empty),
			new KanBanSection("Done", false, String.Empty),
		};


	

		private List<KanbanTaskItem> _tasks = new()
		{
			new KanbanTaskItem("Write unit test", "To Do"),
			new KanbanTaskItem("Some docu stuff", "To Do"),
			new KanbanTaskItem("Walking the dog", "To Do"),
		};

		KanBanNewForm newSectionModel = new KanBanNewForm();

		

		private void OnValidSectionSubmit(EditContext context)
		{
			_sections.Add(new KanBanSection(newSectionModel.Name, false, String.Empty));
			newSectionModel.Name = string.Empty;
			_addSectionOpen = false;
		}

		private void OpenAddNewSection()
		{
			_addSectionOpen = true;
		}

		private void AddTask(KanBanSection section)
		{
			_tasks.Add(new KanbanTaskItem(section.NewTaskName, section.Name));
			section.NewTaskName = string.Empty;
			section.NewTaskOpen = false;
			_dropContainer.Refresh();
		}

		private void DeleteSection(KanBanSection section)
		{
			if (_sections.Count == 1)
			{
				_tasks.Clear();
				_sections.Clear();
			}
			else
			{
				int newIndex = _sections.IndexOf(section) - 1;
				if (newIndex < 0)
				{
					newIndex = 0;
				}

				_sections.Remove(section);

				var tasks = _tasks.Where(x => x.Status == section.Name);
				foreach (var item in tasks)
				{
					item.Status = _sections[newIndex].Name;
				}
			}
		}
	}
}
