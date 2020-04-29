using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebSIte.Models;

namespace TestWebSIte.Data
{
    public class BoardDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ApprovedOrder> ApprovedOrders { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Inquire> Inquires { get; set; }
        public DbSet<Answer> Answers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=192.168.100.151;Database=BoardDbContext-2;User Id=nmasteruser;Password=rhrortpsxj;");
        }
    }
}
