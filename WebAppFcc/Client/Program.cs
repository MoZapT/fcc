using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebAppFcc.Data.DataServices;
using WebAppFcc.Shared.Interfaces.DataServices;

namespace WebAppFcc.Client
{
    public class Program
    {
        protected Program()
        {
        }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("WebAppFcc.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            AddDependencies(builder);

            builder.Services.AddApiAuthorization()
                .AddAccountClaimsPrincipalFactory<CustomClaimsFactory>();

            await builder.Build().RunAsync();
        }

        public static void AddDependencies(WebAssemblyHostBuilder builder)
        {
            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAppFcc.ServerAPI"));

            builder.Services.AddScoped<IPersonDataService, PersonDataService>((sp) => 
                new PersonDataService(sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAppFcc.ServerAPI")));
        }
    }
}
