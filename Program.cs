using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Kestrel settings (optional if running locally + Azure)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
    options.ListenAnyIP(5001, listenOpts => listenOpts.UseHttps());
});

// 1. Connect to SQL Server database
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Serve static files (wwwroot)
app.UseStaticFiles();
app.UseDefaultFiles();

// 2. API Endpoints

// Get all products
app.MapGet("/api/products", async (AppDbContext db) =>
    Results.Ok(await db.Products.ToListAsync())
);

// User registration (plain password)
app.MapPost("/api/account/register", async (User newUser, AppDbContext db) =>
{
    if (await db.Users.AnyAsync(u => u.Email == newUser.Email))
        return Results.BadRequest("Email already registered.");

    db.Users.Add(newUser);
    await db.SaveChangesAsync();
    return Results.Ok();
});

// User login (plain password check)
app.MapPost("/api/account/login", async (User credentials, AppDbContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Email == credentials.Email);

    if (user == null || user.PasswordHash != credentials.PasswordHash)
        return Results.BadRequest("Invalid email or password.");

    return Results.Ok();
});

// Fallback to index.html for frontend routing
app.MapFallbackToFile("index.html");

app.Run();
