using CleanArchitectureCQRS.Application.Handlers;
using CleanArchitectureCQRS.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("CleanArchitectureDb"));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddMediatR(typeof(GetAllProductsQueryHandler).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
