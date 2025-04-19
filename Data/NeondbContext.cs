using Microsoft.EntityFrameworkCore;
using library_management.Models;

namespace library_management.Data
{
    public class NeondbContext : DbContext
    {
        public NeondbContext(DbContextOptions<NeondbContext> options)
            : base(options)
        {
        }

        // DbSets with null-forgiving operator to suppress warnings
        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Loan> Loans { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new ArgumentNullException(nameof(modelBuilder));

            // Book-Author relationship
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(b => b.Author)
                    .WithMany(a => a.Books)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Additional book configurations
                entity.Property(b => b.Title).IsRequired().HasMaxLength(200);
                entity.Property(b => b.ISBN).HasMaxLength(13);
                entity.HasIndex(b => b.ISBN).IsUnique();
            });

            // Loan relationships
            modelBuilder.Entity<Loan>(entity =>
            {
                // Book relationship
                entity.HasOne(l => l.Book)
                    .WithMany(b => b.Loans)
                    .HasForeignKey(l => l.BookId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Member relationship
                entity.HasOne(l => l.Member)
                    .WithMany(m => m.Loans)
                    .HasForeignKey(l => l.MemberId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // Default values
                entity.Property(l => l.LoanDate)
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(l => l.DueDate)
                    .HasDefaultValueSql("DATEADD(day, 14, GETDATE())");
            });

            // Author configurations
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(a => a.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(a => a.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Member configurations
            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(m => m.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(m => m.LastName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(m => m.Email)
                    .IsRequired();
                entity.Property(m => m.PhoneNumber)
                    .IsRequired();
                entity.Property(m => m.RegistrationDate)
                    .HasDefaultValueSql("GETDATE()");
                
                entity.HasIndex(m => m.Email).IsUnique();
            });
        }
    }
}