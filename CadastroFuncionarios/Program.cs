using CadastroFuncionarios.Models.Services;
using Funcionarios.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<LogService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new LogService(
        config.GetConnectionString("DefaultStorage"),
        "FuncionarioLogs");
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["StorageConnection:blobServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddQueueServiceClient(builder.Configuration["StorageConnection:queueServiceUri"]!).WithName("StorageConnection");
    clientBuilder.AddTableServiceClient(builder.Configuration["StorageConnection:tableServiceUri"]!).WithName("StorageConnection");
});

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
    pattern: "{controller=Funcionarios}/{action=Index}/{id?}");

app.Run();
