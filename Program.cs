
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new EthereumService("https://rpc-goerli.mytestrpc.io"));
builder.Services.AddControllers();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
