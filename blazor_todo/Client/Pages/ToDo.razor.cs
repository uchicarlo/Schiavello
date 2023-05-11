using blazor_todo.Client.Services;
using blazor_todo.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using static System.Collections.Specialized.BitVector32;

namespace blazor_todo.Client.Pages
{
	public partial class ToDo
	{
		[Inject] ITodoServices _todoServices { get; set; }
		private MudDropContainer<KanbanTaskItem> _dropContainer;

		private bool _addSectionOpen;
		/* Setup for board  */
		private List<KanBanSection> _sections = new();
		private List<KanbanTaskItem> _tasks = new();
		KanBanNewForm newSectionModel = new KanBanNewForm();
		/* handling board events */

		protected override async Task OnInitializedAsync()
		{
			var response = await _todoServices.GetKanban();
			if (response == null) return;
			_sections = response.kanbanSections;
			_tasks = response.kanbanTaskItems;
		}


		private async Task TaskUpdated(MudItemDropInfo<KanbanTaskItem> info)
		{
			info.Item.Status = info.DropzoneIdentifier;
			var response = await _todoServices.UpdateTask(info.Item);
			_tasks = response;
			StateHasChanged();
		}

		private async Task AddTask(KanBanSection section)
		{
			_tasks.RemoveAll(x => x.Status == section.Name);
			var response = await _todoServices.AddTask(new KanbanTaskItem(section.NewTaskName, section.Name));
			_tasks = response;
			section.NewTaskName = string.Empty;
			section.NewTaskOpen = false;
			_dropContainer.Refresh();
			StateHasChanged();
		}
		private async Task UpdateTask(KanbanTaskItem item)
		{
			_tasks.RemoveAll(x => x.Status == item.Status);
			var response = await _todoServices.UpdateTask(item);
			_tasks = response;
			_dropContainer.Refresh();
		}
		private async Task DeleteTask(KanbanTaskItem item)
		{
			_tasks.RemoveAll(x => x.Status == item.Status);
			var response = await _todoServices.DeleteTask(item);
			_tasks = response;
			_dropContainer.Refresh();
		}
		

		private async Task OnValidSectionSubmit(EditContext context)
		{
			var response = await _todoServices.AddSection(new KanBanSection(newSectionModel.Name, false, String.Empty));
			_sections = response.kanbanSections;
			_tasks = response.kanbanTaskItems;
			newSectionModel.Name = string.Empty;
			_addSectionOpen = false;
			StateHasChanged();
		}

		private void OpenAddNewSection()
		{
			_addSectionOpen = true;
		}
		private async Task DeleteSection(KanBanSection section)
		{
			var response = await _todoServices.DeleteSection(section); 
			_tasks = response.kanbanTaskItems;
			_sections = response.kanbanSections;
			StateHasChanged();
		}
	}
}
