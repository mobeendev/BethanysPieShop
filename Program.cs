using BethanysPieShop.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IPieRepository, PieRepository>();

builder.Services.AddScoped<IShoppingCart, ShoppingCart>(sp => ShoppingCart.GetCart(sp));
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<BethanysPieShopDbContext>(options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:BethanysPieShopDbContextConnection"]);
});


// Configuration setup (replace with your configuration loading logic)
// ConfigurationManager configuration = new ConfigurationManager()
//     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// builder.Services.ConfigureDbContext(configuration.GetConnectionString("BethanysPieShopDbContextConnection")); // Replace "DefaultConnection" with your actual connection string name

// ConfigurationManager configuration = new ConfigurationManager()
//     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// builder.Services.AddDbContext<BethanysPieShopDbContext>(options => options.UseSqlite(configuration.GetConnectionString("BethanysPieShopDbContextConnection")));



// Configuration setup (replace with your configuration loading logic)
// var configurationBuilder = new ConfigurationBuilder()
//     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// IConfiguration configuration = configurationBuilder.Build(); // Build the configuration

// builder.Services.AddDbContext<YourDbContext>(options => options.UseSqlite(configuration.GetConnectionString("BethanysPieShopDbContextConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

DbInitializer.Seed(app);
app.Run();
