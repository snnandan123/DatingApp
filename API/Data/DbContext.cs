using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DataContext : DbContext
{
    //udemy
    // public DataContext(DbContextOptions options):base(options)
    // {
        
    // }

//chatgpt
public DataContext(DbContextOptions<DataContext> options):base(options)
{
    
}
   public DbSet<AppUser> AppUsers {get;set;}
   //on model creating gpt
//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlite("Data Source=DatingApp.db");
//     }
}
