using Microsoft.OpenApi.Models;
using RpaWorker;
using RpaWorker.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IScrapingService, ScrapingService>();
builder.Services.AddHostedService<ScrapingWorker>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "RpaApi", Version = "v1" });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RpaApi v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Console.WriteLine("============================================================");
Console.WriteLine("RpaApi inciado com sucesso! Acesse em http://localhost:7034/api/scrapping");
Console.WriteLine("============================================================");

app.Run();