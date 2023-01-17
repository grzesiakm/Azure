using System.Configuration;
using BookRating.App.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddSingleton<ICosmosDbService>(options =>
// {
//     var url = builder.Configuration.GetSection("CosmosDb")
//         .GetValue<string>("Account");
//     var primaryKey = builder.Configuration.GetSection("CosmosDb")
//         .GetValue<string>("Key");
//     var dbName = builder.Configuration.GetSection("CosmosDb")
//         .GetValue<string>("DatabaseName");
//     var containerName = builder.Configuration.GetSection("CosmosDb")
//         .GetValue<string>("ContainerName");
//
//     var cosmosClient = new CosmosClient(url, primaryKey);
//
//     return new CosmosDbService(cosmosClient, dbName, containerName);
// });

builder.Services.AddSingleton<ICosmosDbService, MockCosmosDbService>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();