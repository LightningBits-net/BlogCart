using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SharedServices.Repository;
using SharedServices.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using SharedServices.Data;
using MudBlazor.Services;
using ECommerce_Server.Service.IService;
using ECommerce_Server.Service;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDbInitializer, DbInitializer >();
builder.Services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IFileUpload, FileUpload>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMudServices();
builder.Services.AddSyncfusionBlazor();


var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("OTMxOTIxQDMyMzAyZTM0MmUzMGkwS29SRUl4VXd4RFQ2ZmhRSEhIQXU1U0pLS0hxQjVUQjZ3RFBlZXRaUmM9");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

SeedDataBase();
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


void SeedDataBase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}