using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace FileProcessor.Data
{
    public class FileProcessorDbContext : DbContext
    {
        public FileProcessorDbContext(DbContextOptions<FileProcessorDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ApiKey> ApiKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiKey>(entity =>
            {
                entity.ToTable("ApiKey");
                entity.HasKey(e => e.Id);
            });
        }
    }
}
