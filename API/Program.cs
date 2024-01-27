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

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
