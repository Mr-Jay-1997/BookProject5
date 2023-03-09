using DALBookProject.Database.Tables;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALBookProject.Database
{
    public partial class BookDbContext : DbContext
    {
        public class optionBuild
        {
            public optionBuild()
            {
                settings = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<BookDbContext>();
                opsBuilder.UseSqlServer(settings.DbConnString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<BookDbContext> opsBuilder { get; set; }

            public DbContextOptions<BookDbContext> dbOptions { get; set; }

            private AppConfiguration settings { get; set; }
        }

        public static optionBuild ops = new optionBuild();

        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {
            //Database.SetCommandTimeout(60);
        }

        public virtual DbSet<BOOK> Books { get; set; }

        public virtual DbSet<CATEGORY> Categories { get; set; }

        public virtual DbSet<USER> Users { get; set; }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BOOK>(entity =>
            {
                entity.ToTable("BOOK");
                entity.HasKey(e => e.bookid);
                entity.Property(e => e.bookid).HasColumnName("book_id").IsRequired().HasColumnType("int");
                entity.Property(e => e.bookname).HasColumnName("book_name").IsRequired().HasMaxLength(50);
                entity.Property(e => e.categoryid).HasColumnName("category_id").IsRequired().HasColumnType("int");
                entity.Property(e => e.author).HasMaxLength(50);
                entity.Property(e => e.publisher).HasMaxLength(50);
                entity.Property(e => e.price).IsRequired().HasColumnType("decimal");
                //entity.HasOne(e => e.CATEGORY).WithOne(d => d.BOOK).HasForeignKey("categoryid");
            });

            modelBuilder.Entity<CATEGORY>(entity =>
            {
                entity.ToTable("CATEGORY");
                entity.HasKey(e => e.categoryid);
                entity.Property(e => e.categoryid).HasColumnName("category_id").IsRequired().HasColumnType("int");
                entity.Property(e => e.categoryname).HasColumnName("category_name").IsRequired().HasMaxLength(50);
            
            });


            modelBuilder.Entity<USER>(entity =>
            {
                entity.ToTable("USER");
                entity.HasKey(e => e.userid);
                entity.Property(e => e.userid).HasColumnName("user_id").IsRequired().HasColumnType("int");
                entity.Property(e => e.firstname).HasColumnName("first_name").IsRequired().HasMaxLength(50);
                entity.Property(e => e.lastname).HasColumnName("last_name").HasMaxLength(50);
                entity.Property(e => e.email).HasColumnName("email").IsRequired().HasMaxLength(50);
                entity.Property(e => e.password).HasColumnName("password").IsRequired().HasMaxLength(50);
                entity.Property(e => e.mobile).HasMaxLength(50);
                
                
            });
            OnModelCreatingPartial(modelBuilder);
            
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
