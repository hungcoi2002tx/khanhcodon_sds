using GrpcServer.Repository;
using GrpcServer.Services;
using ProtoBuf.Grpc.Server;
using Share;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
//builder.Services.AddSingleton<ILopHocServiceGrpc, LopHocService>();
builder.Services.AddSingleton<ILopHocRepository, LopHocRepository>();
builder.Services.AddCodeFirstGrpc();

builder.Services.AddGrpcReflection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<LopHocService>();

app.MapGrpcReflectionService();
app.MapGet("/", () => "GrpcServer");

app.Run();
