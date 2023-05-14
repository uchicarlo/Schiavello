using blazor_todo.Shared.Interface;
using blazor_todo.Shared.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Threading.Tasks;

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
            StateHasChanged();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.UpdateTask(info.Item);
                _tasks = response;
                await InvokeAsync(StateHasChanged);
            });

        }

        private async Task AddTask(KanBanSection section)
        {
            if (string.IsNullOrWhiteSpace(section.NewTaskName))
            {
                section.NewTaskName = string.Empty;
                section.NewTaskOpen = false;
                StateHasChanged();
                _dropContainer.Refresh();
                return;
            }
            var task = new KanbanTaskItem(section.NewTaskName, section.Name);
            _tasks.Add(task);
            StateHasChanged();
            _dropContainer.Refresh();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.AddTask(task);
                _tasks = response;
                section.NewTaskName = string.Empty;
                section.NewTaskOpen = false;
                await InvokeAsync(StateHasChanged);
                _dropContainer.Refresh();
            });
        }
        private async Task DeleteTask(KanbanTaskItem item)
        {
            _tasks.Remove(item);
            StateHasChanged();
            _dropContainer.Refresh();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.DeleteTask(item);
                _tasks = response;
                await InvokeAsync(StateHasChanged);
                _dropContainer.Refresh();
            });

        }
        private async Task OnValidTaskSubmit(EditContext context)
        {
            var item = (KanbanTaskItem)context.Model;
            var task = _tasks.FirstOrDefault(x => x.Id == item.Id) ?? new();
            task = item;
            StateHasChanged();
            _dropContainer.Refresh();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.UpdateTask(item);
                _tasks = response;
                newSectionModel.Name = string.Empty;
                _addSectionOpen = false;
                await InvokeAsync(StateHasChanged);
                _dropContainer.Refresh();
            });
        }
        private async Task OnValidSectionEditSubmit(EditContext context)
        {
            var section = (KanBanSection)context.Model;
            var response = await _todoServices.UpdateSection(section);
            _sections = response.kanbanSections;
            _tasks = response.kanbanTaskItems;
            StateHasChanged();
            _dropContainer.Refresh();
        }

        private async Task OnValidSectionSubmit(EditContext context)
        {
            var section = new KanBanSection(newSectionModel.Name, false, String.Empty);
            _sections.Add(section);
            StateHasChanged();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.AddSection(section);
                _sections = response.kanbanSections;
                _tasks = response.kanbanTaskItems;
                newSectionModel.Name = string.Empty;
                _addSectionOpen = false;
                await InvokeAsync(StateHasChanged);
            });
        }
        private void EditTask(KanbanTaskItem item)
        {
            item.IsEditing = !item.IsEditing;
            StateHasChanged();
            _dropContainer.Refresh();
        }

        private void OpenAddNewSection()
        {
            _addSectionOpen = true;
        }
        private async Task DeleteSection(KanBanSection section)
        {
            _sections.Remove(section);
            StateHasChanged();
            await Task.Delay(1);
            await Task.Run(async () =>
            {
                var response = await _todoServices.DeleteSection(section);
                _tasks = response.kanbanTaskItems;
                _sections = response.kanbanSections;
                await InvokeAsync(StateHasChanged);
                _dropContainer.Refresh();
            });
        }
        private void EditSection(KanBanSection section)
        {
            section.IsEditing = !section.IsEditing;
            StateHasChanged();
        }

        private void CancelAddingSection(KanBanSection section)
        {
            section.NewTaskName = string.Empty;
            section.NewTaskOpen = false;
            StateHasChanged();
        }
    }
}
