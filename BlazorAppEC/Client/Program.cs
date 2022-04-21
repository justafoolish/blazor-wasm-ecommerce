using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorAppEC.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using Blazored.Toast;
using BlazorAppEC.Shared.Services;


namespace BlazorAppEC.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddScoped<AuthenticationStateProvider, BlazorAppEC.Client.CustomAuthenticationStateProvider>();

            
            //client
            builder.Services.AddSingleton<ICategoryManageVM, CategoryManageVM>();
            builder.Services.AddSingleton<BlazorAppEC.Shared.IUserLoginVM, UserLoginVM>();
            builder.Services.AddSingleton<IUserRegisterVM, UserRegisterVM>();
            builder.Services.AddSingleton<IHomePageVM, HomePageVM>();
            builder.Services.AddHttpClient<ICatalogVM, CatalogVM>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
            builder.Services.AddScoped<ICartService, CartService>();
            builder.Services.AddHttpClient<ICheckoutVM, CheckoutVM>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            //admin
            builder.Services.AddSingleton<IProductManageVM, ProductManageVM>();
            builder.Services.AddSingleton<IProductAddVM, ProductAddVM>();
            builder.Services.AddSingleton<ICategoryAddVM, CategoryAddVM>();
            builder.Services.AddSingleton<ICategoryManageVM, CategoryManageVM>();
            await builder.Build().RunAsync();
        }
    }
}
