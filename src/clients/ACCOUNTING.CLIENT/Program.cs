using ACCOUNTING.CORE.Contracts;
using ACCOUNTING.CORE.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace ACCOUNTING.CLIENT
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddMudServices();
            builder.Services.AddTransient<IGeneralService,GeneralService>();
            builder.Services.AddTransient<IAgentInfoService,AgentInfoService>();
            builder.Services.AddTransient<IAgentCloseService,AgentCloseService>();
            builder.Services.AddTransient<IAgentDuplicateService, AgentDuplicateService>();
            await builder.Build().RunAsync();
        }
    }
}
