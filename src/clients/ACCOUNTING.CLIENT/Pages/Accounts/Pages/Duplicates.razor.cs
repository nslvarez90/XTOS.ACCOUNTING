using ACCOUNTING.CLIENT.Pages.Accounts.Dialogs;
using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ACCOUNTING.CLIENT.Pages.Accounts.Pages
{
    public partial class Duplicates
    {
        [Inject] private IAgentInfoService AgentInfoService { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        public record Employee(string MainAgent, string DuplicateAgent, WeekDTO Weeks, WeekDTO Weeks2, WeekDTO Weeks3, WeekDTO Weeks4, double Percent, string IPs);
        public IEnumerable<Employee> employees;
        public IEnumerable<AgentInfoDTO> agents;
        public IEnumerable<WeekDTO> weeksList;
        private string _searchString;
        private string _searchStringEnabled="";
        private bool _sortMainAgentByLength;
        private List<string> _events = new();
        public int index = 0;
        public int totalToShow { get; set; } = 3;
        public bool payments { get; set; } = false;
        // quick filter - filter globally across multiple columns with the same input

        private string _buttonText = "4 weeks";
        private bool visible { get; set; } = false;
        private bool dataLoaded { get; set;} =false;
        MudDataGrid<AgentInfoDTO> agentsList;

        private Func<Employee, bool> QuickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
                return true;

            if (x.MainAgent.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if (x.DuplicateAgent.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
                return true;

            if ($"{x.Percent} {x.IPs}".Contains(_searchString))
                return true;

            return false;
        };

        /* private Func<AgentInfoDTO, bool> QuickFilterEnanledAgents => x =>
         {
             if (string.IsNullOrWhiteSpace(_searchStringEnabled))
                 return true;

             if (x.AgentName.Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase))
                 return true;

             if (x.Balance.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase))
                 return true;

             if (x.BalancePeriod.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase))
                 return true;
             if (x.Fee.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase))
                 return true;
             if (x.TotalPayment.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase))
                 return true;
             if (x.WeeksInfo.Any(w =>
               w.ActivePlayer.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase) ||
               w.Payment.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase) ||
               w.Charge.ToString().Contains(_searchStringEnabled, StringComparison.OrdinalIgnoreCase)
               ))
                 return true;



             return false;
         };*/
        private Func<AgentInfoDTO, bool> QuickFilterEnabledAgents => x =>
        {
            var searchString = _searchStringEnabled.Trim();

            if (string.IsNullOrEmpty(searchString))
                return true;

            return x.AgentName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   x.Balance.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   x.BalancePeriod.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   x.Fee.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   x.TotalPayment.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                   x.WeeksInfo.Any(w =>
                       w.ActivePlayer.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                       w.Payment.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                       w.Charge.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase)
                   );
        };

        public string Get_searchStringEnabled()
        {
            return _searchStringEnabled;
        }


        protected override async Task OnInitializedAsync()
        {
            try
            {
                agents = await AgentInfoService.GetActiveAgents(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: --->{ex.Message}");
            }
            var week = new WeekDTO { ActivePlayer = 10, Charge = 40, Payment = 200 };
            var week2 = new WeekDTO { ActivePlayer = 30, Charge = 10, Payment = 500 };
            var week3 = new WeekDTO { ActivePlayer = 90, Charge = 500, Payment = 400 };
            var week4 = new WeekDTO { ActivePlayer = 70, Charge = 800, Payment = 60 };
            employees = new List<Employee>
             {
              new Employee("Alice", "Alicet",week,week2,week3,week4, 0.8, "10.8.126.35"),
               new Employee("Irak", "Iraks",week,week3,week2, week4, 0.6, "10.8.126.31"),
              new Employee("John", "Johns",week3,week2,week, week4, 0.95, "10.8.126.55,10.8.136.25"),
            };
        }


      
        public List<WeekDTO> getListWeeks(uint agentId)
        {
            return agents.Single(s => s.IdAgent == agentId).WeeksInfo;
        }


        private async Task<IDialogReference> OpenDialogSoftClosed()
        {
            var parameters = new DialogParameters<CloseDialog> { { x => x.typeOf,1 } };
            var options = new DialogOptions { CloseOnEscapeKey = false, BackdropClick = false , MaxWidth = MaxWidth.Large, FullWidth = true };
            return await DialogService.ShowAsync<CloseDialog>("Soft Closed", parameters, options);
        }

        private async Task<IDialogReference> OpenDialogHardClosed()
        {
            var parameters = new DialogParameters<CloseDialog> { { x => x.typeOf, 2 } };
            var options = new DialogOptions { CloseOnEscapeKey = false, BackdropClick = false, MaxWidth = MaxWidth.Large, FullWidth = true };
            return await DialogService.ShowAsync<CloseDialog>("Hard Closed", parameters, options);
        }

        private async Task<IDialogReference> OpenDialogEnabled()
        {
            var parameters = new DialogParameters<CloseDialog> { { x => x.typeOf, 3 } };
            var options = new DialogOptions { CloseOnEscapeKey = false, BackdropClick = false, MaxWidth = MaxWidth.Large, FullWidth = true };
            return await DialogService.ShowAsync<CloseDialog>("Enabled Closed", parameters, options);
        }

        private async Task<IDialogReference> OpenDialogDuplicate()
        {
            //var parameters = new DialogParameters<CloseDialog> { { x => x.typeOf, 3 } };
            var options = new DialogOptions { CloseOnEscapeKey = false, BackdropClick = false, MaxWidth = MaxWidth.Large, FullWidth = true };
            return await DialogService.ShowAsync<DuplicateDialog>("Duplicate Accounts", options);
        }

        private async Task<IDialogReference> OpenDialogDecreased() 
        {
            var options = new DialogOptions { CloseOnEscapeKey = false, BackdropClick = false, MaxWidth = MaxWidth.Large, FullWidth = true };
            return await DialogService.ShowAsync<ActivePlayerDialog>("Active Player Decrease", options);
        }
        public async void OpenOverlay()
        {
            visible = true;
            dataLoaded = false;
            await Task.Delay(3000);
            dataLoaded = true;
            visible = false;
        }


        private async Task SetButtonTextAsync(int id)
        {
            //agents = [];
            //StateHasChanged();
            switch (id)
            {
                case 0:
                    _buttonText = "4 Weeks";
                    totalToShow = 3;                    
                    break;
                case 1:
                    _buttonText = "8 Weeks";                  
                    totalToShow = 7;
                    break;
                case 2:
                default:
                    break;
            }
            StateHasChanged();
        }
    }
}
