using Microsoft.EntityFrameworkCore;
using Minimal_API.Data;
//using static Minimal_API.Data.Minimal_APIData;

string appsettingFile = "appsettings.json";
#if (!DEBUG)
            appsettingFile = "appsettings.Production.json";
#endif


var builder = WebApplication.CreateBuilder(args);
                //.AddJsonFile(appsettingFile, optional: false, reloadOnChange: true)
                //.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<Minimal_APIData>(options =>
    options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=master;Integrated Security=True;Trust Server Certificate=True"));

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IMinimal_APIData, Minimal_APIData>();

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

app.UseRouting();
//app.UseEndpoints(_ => { });

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{Controller=Tareas}/{action=GET}/{id?}");

//app.MapGet("/one/two", (string a) => $"Hello World: {a}");

app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
