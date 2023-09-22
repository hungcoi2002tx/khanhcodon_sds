using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QuanLySinhVien.Data;
using Blazored.Toast;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Hosting.Server;
using QuanLySinhVien.Repository;
using QuanLySinhVien.Service;
using ProtoBuf.Grpc.Client;
using Share;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredToast();
builder.Services.AddSingleton<ISinhVienService, SinhVienService>();
builder.Services.AddSingleton<ISinhVienRepository, SinhVienRepository>();
builder.Services.AddSingleton<ILopHocService, LopHocService>();
builder.Services.AddSingleton<ILopHocRepository, LopHocRepository>();
builder.Services.AddAntDesign();

builder.Services.AddTransient<ILopHocServiceGrpc>(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<ILopHocServiceGrpc>();
});


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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

