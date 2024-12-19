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
    partial class DuplicateDialog
    {
        [CascadingParameter]
        private MudDialogInstance MudDialog { get; set; }

        [Inject] private IAgentDuplicateService AgentDuplicateService { get; set; }

       

        public IEnumerable<DuplicateInfoDTO> agentsDuplicates;
        private string _searchString;
        private bool _sortMainAgentByLength;
        private List<string> _events = new();
        
        private Func<DuplicateInfoDTO, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;
            if (x.MainAgentName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.DuplicateAgentName.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (x.Ips.ToString().Contains(_searchString))
                return true;
            if (x.Hours.ToString().Contains(_searchString))
                return true;
            if (x.Password.ToString().Contains(_searchString))
                return true;
            if (x.Bets.ToString().Contains(_searchString))
                return true;
            return false;
        };

        private void Close() => MudDialog.Close(DialogResult.Ok(true));

        void SelectedItemsChanged(HashSet<DuplicateInfoDTO> items)
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Configuration.ShowTransitionDuration = 200;
            Snackbar.Add(items.Count > 0 ? $"Select {items.Last<DuplicateInfoDTO>().MainAgentName}" : "Nothing select", MudBlazor.Severity.Info);

        }
        protected override async Task OnInitializedAsync()
        {
            
            try
            {
                agentsDuplicates = await AgentDuplicateService.GetAgentsDuplicate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: --->{ex.Message}");
            }
        }
    }
}
