using EFCoreCodeFirstDemo.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.MapGet("/", context =>
{
    context.Response.Redirect("/api/products");
    return Task.CompletedTask;
});


// Configure the HTTP request pipeline.

app.UseAuthorization();

//  Ensure database is created & migrations are applied
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // applies migrations, creates DB if not exists
}

app.MapControllers();

app.Run();
