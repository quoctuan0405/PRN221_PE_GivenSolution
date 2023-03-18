using Microsoft.EntityFrameworkCore;
using Q2.Hub;
using Q2.Models;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30);
});

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseSession();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapHub<>("");
});

app.MapRazorPages();
app.MapHub<MovieHub>("/moviehub");

app.Run();
