
// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllersWithViews();

// var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// // app.UseSession();

// // Define your routes
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Login}/{action=userlogin}");

// app.Run();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Explicitly specify the HTTP and HTTPS ports
var urls = new List<string> { "http://localhost:5020", "https://localhost:5021" };
// app.Urls.Clear();
// app.Urls.AddRange(urls);

// Configure HTTPS redirection
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseSession();
app.MapRazorPages();
app.MapDefaultControllerRoute();
app.Run();
