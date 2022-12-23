using FealtedByEsra.Entity.Entities;
using FealtedByEsra.Entity.Entities.Common;
using FealtedByEsra.Entity.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FealtedByEsra.DAL.Concrete.Context
{
    public class FealtedByEsraDbContext : IdentityDbContext<AppUser>
    {
        public FealtedByEsraDbContext(DbContextOptions<FealtedByEsraDbContext> options) : base(options)
        { }

        public DbSet<Post>? Posts { get; set; }

        public DbSet<Category>? Categories { get; set; }

        public DbSet<Comment>? Comments { get; set; }

        public DbSet<ReplyComment>? ReplyComments { get; set; }

        public DbSet<Newsletter>? Newsletters { get; set; }

        public DbSet<Contact>? Contacts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=77.245.159.27\\MSSQLSERVER2019;Database=FealtedByEsraDb;User=esraAdmin ;Password=t%3vOe073");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasSequence("FE_Sequence");

            builder.Entity<Post>()
                .Property(p => p.Id)
                .HasDefaultValueSql("NEXT VALUE FOR FE_Sequence");

            builder.Entity<Post>()
                .Property(p => p.Title)
                .HasColumnName("Başlık");

            builder.Entity<Post>()
                .Property(p => p.Description)
                .HasColumnName("Açıklama");

            builder.Entity<Category>()
                .Property(c => c.CategoryName)
                .HasColumnName("KategoriAdi");

            builder.Entity<Post>()
                .Navigation(p => p.Category)
                .AutoInclude();

            builder.Entity<Category>()
                .Navigation(c => c.Posts)
                .AutoInclude();

            builder.Entity<Comment>()
                .Navigation(c => c.Post)
                .AutoInclude();

            builder.Entity<Comment>()
                .Navigation(c => c.ReplyComments)
                .AutoInclude();

            builder.Entity<Newsletter>()
                .HasIndex(n => n.Email)
                .IsUnique();

            builder.Entity<Category>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEXT VALUE FOR FE_Sequence");

            builder.Entity<Comment>()
                .Property(c => c.Id)
                .HasDefaultValueSql("NEXT VALUE FOR FE_Sequence");

            builder.Entity<Newsletter>()
            .Property(c => c.Id)
            .HasDefaultValueSql("NEXT VALUE FOR FE_Sequence");

            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var item in datas)
            {
                _ = item.State switch
                {
                    EntityState.Added => item.Entity.CreatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
