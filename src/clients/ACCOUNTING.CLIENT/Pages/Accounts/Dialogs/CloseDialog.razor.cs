using ACCOUNTING.CLIENT.Pages.Accounts.Models;
using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Models;
using ACCOUNTING.CORE.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.NetworkInformation;
using System.Reflection;
using static MudBlazor.CategoryTypes;

namespace ACCOUNTING.CLIENT.Pages.Accounts.Dialogs
{
    partial class CloseDialog
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject] private IAgentCloseService AgentCloseService { get; set; }

        private string action = "Close selected Soft";
        private string icon = Icons.Material.Filled.CloudCircle;
        private Color color = Color.Warning;

        [Parameter]
        public int typeOf { get; set; }

        public IEnumerable<AgentCloseDTO> agentsToClose;
        private string _searchString;
        private bool _sortMainAgentByLength;
        private List<string> _events = new();
        public int weeks { get; set; } = 4;
        public bool payments { get; set; } = false;
        // quick filter - filter globally across multiple columns with the same input
        private Func<AgentCloseDTO, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.AgentName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.RootDGS.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.SubAgents.Contains(_searchString))
                return true;
            if (x.Balance.ToString().Contains(_searchString))
                return true;

            return false;
        };

        private void Close() => MudDialog.Close(DialogResult.Ok(true));

        void SelectedItemsChanged(HashSet<AgentCloseDTO> items)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Configuration.ShowTransitionDuration = 200;
            Snackbar.Add(items.Count > 0 ? $"Select {items.Last<AgentCloseDTO>().AgentName}" : "Nothing select", MudBlazor.Severity.Info);

        }
        protected override async Task OnInitializedAsync()
        {
            switch (typeOf)
            {
                case 1:
                    action = "Close selected Soft";
                    icon = Icons.Material.Filled.CloudCircle;
                    color = Color.Warning;
                    break;
                case 2:
                    action = "Close selected Hard";
                    icon = Icons.Material.Filled.Hardware;
                    color = Color.Error;
                    break;
                case 3:
                    action = "Enable selected";
                    icon = Icons.Material.Filled.CheckCircle;
                    color = Color.Success;
                    break;
                default:
                    break;
            }

            try
            {
                agentsToClose = await AgentCloseService.GetAgentsToClose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: --->{ex.Message}");
            }
        }
    }
}
