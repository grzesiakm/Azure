using BookRating.App.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ICosmosDbService>(options =>
{
    var url = builder.Configuration.GetSection("AzureCosmosDbSettings")
        .GetValue<string>("URL");
    var primaryKey = builder.Configuration.GetSection("AzureCosmosDbSettings")
        .GetValue<string>("PrimaryKey");
    var dbName = builder.Configuration.GetSection("AzureCosmosDbSettings")
        .GetValue<string>("DatabaseName");
    var containerName = builder.Configuration.GetSection("AzureCosmosDbSettings")
        .GetValue<string>("ContainerName");

    var cosmosClient = new CosmosClient(url, primaryKey);

    return new CosmosDbService(cosmosClient, dbName, containerName);
});

// builder.Services.AddSingleton<ICosmosDbService, MockCosmosDbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/BookRating/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BookRating}/{action=Index}/{id?}");

app.Run();