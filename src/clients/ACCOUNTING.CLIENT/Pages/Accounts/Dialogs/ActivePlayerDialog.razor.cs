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
    partial class ActivePlayerDialog
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject] private IAgentWeeksService AgentWeeksService { get; set; }

       

        public IEnumerable<AgentWeeksDTO> playersWeeks;
        private string _searchString;
        private bool _sortMainAgentByLength;
        private List<string> _events = new();
        
        private Func<AgentWeeksDTO, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (x.IdAgent.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.AgentName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.PlayerTotal.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Week1.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Week2.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Week3.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Week4.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        };

        private void Close() => MudDialog.Close(DialogResult.Ok(true));

        void SelectedItemsChanged(HashSet<AgentWeeksDTO> items)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Configuration.ShowTransitionDuration = 200;
            Snackbar.Add(items.Count > 0 ? $"Select {items.Last<AgentWeeksDTO>().AgentName}" : "Nothing select", MudBlazor.Severity.Info);

        }
        protected override async Task OnInitializedAsync()
        {
            
            try
            {
                playersWeeks = await AgentWeeksService.GetAgentsWeeks();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: --->{ex.Message}");
            }
        }
    }
}
