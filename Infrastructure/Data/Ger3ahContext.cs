using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class Ger3ahContext : DbContext
    {
        public Ger3ahContext(DbContextOptions<Ger3ahContext> options) : base
        (options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ger3ahName> Ger3ahNames { get; set; }
        public DbSet<Ger3ahLog> Ger3ahLogs { get; set; }
        public DbSet<PickerChrecer> PickerChrecer { get; set; }
    }
}