using ApiAuth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //the change occurs here.
    //builder.cofiguration and not just configuration
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddAuthentication("Identity.Login")
    .AddCookie("Identify.Login", config =>
    {
        config.Cookie.Name = "Identity.Login";
        config.LoginPath = "/Login";
        config.AccessDeniedPath = "/Home";
       
    });
builder.Services.AddConnections();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
