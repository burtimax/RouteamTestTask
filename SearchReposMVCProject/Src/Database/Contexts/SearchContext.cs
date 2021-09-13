using Microsoft.EntityFrameworkCore;
using SearchReposMVCProject.Src.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchReposMVCProject.Src.Database.Contexts
{
    public class SearchContext : DbContext
    {
    

        public DbSet<Search> Search { get; set; }

        public SearchContext(DbContextOptions<SearchContext> options): base(options)
        {
            Database.EnsureCreated();
        }
        
        // Подключаемся в Startup.cs
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=0001");
        //}
    }
}
