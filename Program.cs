using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add EF Core (keep this)
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 2. Add Controllers for API endpoints (you missed this)
builder.Services.AddControllers();

var app = builder.Build();

// 3. Middleware setup (correct order)

app.UseDefaultFiles();  // serve index.html automatically
app.UseStaticFiles();   // serve css, js, images

// 4. Map your Controllers (missing before!)
app.MapControllers();

// 5. Minimal APIs you already had (products, register, login)

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

// 6. Fallback anything else to index.html
app.MapFallbackToFile("index.html");

app.Run();
