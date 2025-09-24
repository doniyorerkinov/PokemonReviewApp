using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data
{
    // DbContext base .net ichida bo'lmaydi, uni EntityFrameworkdan chaqirib ishlatamiz.
    public class DataContext : DbContext
    {
        // DBContextOptions qandaydir collection va u DataContext class turidagi optionni argument sifatida olyabdi
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Country> Countries { get; set; }
        
        public DbSet<Owner> Owners { get; set; }
        
        public DbSet<Pokemon> Pokemons { get; set; }

        public DbSet<PokemonOwner> PokemonOwners { get; set; }

        public DbSet<PokemonCategory> PokemonCategories { get; set; }
        
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Reviewer> Reviewers { get; set; }

        // Ushbu kod bizga database'dagi many-to-many bog'liqliklarni ko'rsatish uchun kerak.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hozir biz EntityFrameWorkka aytyabmiz, PokemonCategory'da CategoryId va PokemonId'lar bo'lishi muhim.
            modelBuilder.Entity<PokemonCategory>()
                .HasKey(pc => new { pc.CategoryId, pc.PokemonId });

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<PokemonCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.PokemonCategories)
                .HasForeignKey(c => c.CategoryId);


            modelBuilder.Entity<PokemonOwner>()
                .HasKey(po => new { po.PokemonId, po.OwnerId });

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Pokemon)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(c => c.PokemonId);

            modelBuilder.Entity<PokemonOwner>()
                .HasOne(p => p.Owner)
                .WithMany(pc => pc.PokemonOwners)
                .HasForeignKey(c => c.OwnerId);
        }
    }
}
