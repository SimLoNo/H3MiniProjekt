using H3MiniProjekt.DAL.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H3MiniProjekt.DAL.Database
{
    public class AbContext : DbContext
    {
        public AbContext() { }
        public AbContext(DbContextOptions<AbContext> options) : base(options) { }
        // 1) Data til databasen -- seed
        // 2) data fra en klasse til databasen..
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=SIMONNGF2DATA\H2SQLSOMMERSIMON;Database=H3MiniProjekt001; Trusted_Connection=True");
        }
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
    }
}
