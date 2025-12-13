using JoyBoxPlatform.Data;
using JoyBoxPlatform.Models;
using JoyBoxPlatform.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);



// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "JoyBoxPlatform API",
//        Description = "API for JoyBox games platform"
//    });
//});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();




// Services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();





var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JoyBoxPlatform API V1");
    //    c.RoutePrefix = "swagger"; // Swagger still available at /swagger
    //});
}



app.MapControllers();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!db.Games.Any())
    {
        db.Games.AddRange(
            new Game { Title = "Super Fun Game", Description = "A fun game to test." },
            new Game { Title = "Adventure Quest", Description = "An adventurous journey." }
        );
        db.SaveChanges();
    }
}



app.Run();
