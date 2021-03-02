using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("WebAppFcc.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            AddLocalizations(builder);
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
    
        private static void AddLocalizations(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            builder.Services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    List<CultureInfo> supportedCultures =
                        new List<CultureInfo>
                        {
                                        new CultureInfo("de-DE"),
                                        new CultureInfo("ru-RU"),
                                        new CultureInfo("en-US")
                        };

                    options.DefaultRequestCulture = new RequestCulture("de-DE");

                    // Formatting numbers, dates, etc.
                    options.SupportedCultures = supportedCultures;

                    // UI string 
                    options.SupportedUICultures = supportedCultures;
                });
        }
    }
}
