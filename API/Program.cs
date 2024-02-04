using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//udemy not working
// builder.Services.AddDbContext<DataContext>(opt=>{
//     opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
// });

//gpt
// string conStr="Data Source=DatingApp.db";
builder.Services.AddDbContext<DataContext>(opt=>{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultCon"));
});
builder.Services.AddCors();


var app = builder.Build();

app.UseCors(builder=>builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
