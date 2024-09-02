using AvianParkKlere.Components;
using MudBlazor.Services;
using AutoMapper;

using AvianParkKlere.Contracts.AutoMapper;

namespace AvianParkKlere
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Added services
            builder.Services.AddMudServices();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddHttpClient("Default", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7067/");
            });

            // Added Services
            builder.Services.AddAutoMapper(typeof(MapperConfig)); // AutoMapper Configuration

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}