using DemoProject.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Infrastructure.DBContext;

public partial class DemoProjectDbContext : DbContext
{
    public DemoProjectDbContext()
    {
    }

    public DemoProjectDbContext(DbContextOptions<DemoProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-D396GR4H;Database=DemoProjectDb;user=sa;password=123456;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.PublishDateUtc).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(80);

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Book__CategoryId__398D8EEE");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Catalog");

            entity.ToTable("Category");

            entity.Property(e => e.Name).HasMaxLength(80);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
