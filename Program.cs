using CelilCavus.Interfaces;
using CelilCavus.Manager;
using CelilCavus.Models.Entites;
using CelilCavus.MvcCoreAPICrud_1_10.Manager;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<BaseClientManager<Ogrenci>,OgrenciManager>();
        builder.Services.AddScoped<BaseClientManager<Dersler>, DersManager>();
        var app = builder.Build();
        
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}