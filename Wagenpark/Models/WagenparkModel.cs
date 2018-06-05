namespace Wagenpark.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WagenparkModel : DbContext
    {
        public WagenparkModel()
            : base("name=WagenparkModel")
        {
        }

        public virtual DbSet<CAR> CAR { get; set; }
        public virtual DbSet<DEALER> DEALER { get; set; }
        public virtual DbSet<ONDERHOUD> ONDERHOUD { get; set; }
        public virtual DbSet<WERKPLAATS> WERKPLAATS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CAR>()
                .Property(e => e.Kenteken)
                .IsUnicode(false);

            modelBuilder.Entity<CAR>()
                .Property(e => e.Merk)
                .IsUnicode(false);

            modelBuilder.Entity<CAR>()
                .Property(e => e.AutoType)
                .IsUnicode(false);

            modelBuilder.Entity<CAR>()
                .HasMany(e => e.ONDERHOUD)
                .WithRequired(e => e.CAR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DEALER>()
                .Property(e => e.Naam)
                .IsUnicode(false);

            modelBuilder.Entity<DEALER>()
                .HasMany(e => e.CAR)
                .WithRequired(e => e.DEALER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ONDERHOUD>()
                .Property(e => e.Kosten)
                .HasPrecision(4, 2);

            modelBuilder.Entity<ONDERHOUD>()
                .Property(e => e.Kenteken)
                .IsUnicode(false);

            modelBuilder.Entity<WERKPLAATS>()
                .Property(e => e.Naam)
                .IsUnicode(false);

            modelBuilder.Entity<WERKPLAATS>()
                .HasMany(e => e.ONDERHOUD)
                .WithRequired(e => e.WERKPLAATS)
                .WillCascadeOnDelete(false);
        }
    }
}
