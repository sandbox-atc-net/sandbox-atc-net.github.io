using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AtcWeb.Domain;
using AtcWeb.Domain.GitHub;
using AtcWeb.Domain.GitHub.Clients;
using Ganss.XSS;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace AtcWeb
{
    public static class Program
    {
        [SuppressMessage("Minor Code Smell", "S1075:URIs should not be hardcoded", Justification = "OK.")]
        public static Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(_ => new DefaultBrowserOptionsMessageHandler
            {
                DefaultBrowserRequestCache = BrowserRequestCache.NoStore,
                DefaultBrowserRequestMode = BrowserRequestMode.NoCors,
            });

            builder.Services.AddHttpClient(HttpClientConstants.GitHubApiClient, httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://api.github.com");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 Mobile Safari/537.36");
            });

            builder.Services.AddHttpClient(HttpClientConstants.GitHubRawClient, httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://raw.githubusercontent.com");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.106 Mobile Safari/537.36");
            });

            builder.Services.AddScoped<GitHubApiClient>();
            builder.Services.AddScoped<GitHubRawClient>();
            builder.Services.AddScoped<GitHubRepositoryService>();
            builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(x =>
            {
                var sanitizer = new HtmlSanitizer();
                sanitizer.AllowedAttributes.Add("class");
                return sanitizer;
            });

            builder.Services.AddMemoryCache();

            builder.Services.AddMudServices();

            return builder.Build().RunAsync();
        }
    }
}
