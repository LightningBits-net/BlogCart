using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SharedServices.Repository;
using SharedServices.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using SharedServices.Data;
using MudBlazor.Services;
using Syncfusion.Blazor;
//using static MudBlazor.CategoryTypes;
using BlogCart.Service;
using BlogCart.Service.IService;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

