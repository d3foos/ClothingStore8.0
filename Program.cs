using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listenOpts => listenOpts.UseHttps());
});

// 1. Register EF Core
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();
app.UseStaticFiles();
// 2. Serve default/index.html for "/"
app.UseDefaultFiles();
// 3. Serve all static files from wwwroot


// 4. API endpoints
app.MapGet("/api/products", async (AppDbContext db) =>
    Results.Ok(await db.Products.ToListAsync())
);

app.MapPost("/api/account/register", async (User newUser, AppDbContext db) =>
{
    if (await db.Users.AnyAsync(u => u.Email == newUser.Email))
        return Results.BadRequest("Email already registered.");

    db.Users.Add(newUser);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapPost("/api/account/login", async (User credentials, AppDbContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);
    if (user == null || user.PasswordHash != credentials.PasswordHash)
        return Results.BadRequest("Invalid email or password.");
    return Results.Ok();
});

// 5. Fallback any other route to index.html
app.MapFallbackToFile("index.html");

app.Run();
