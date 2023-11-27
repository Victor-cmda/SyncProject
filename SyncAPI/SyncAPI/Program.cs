using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SyncAPI.Data;
using SyncAPI.Interfaces;
using SyncAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SyncAPIDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IVisitasService, VisitasService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

