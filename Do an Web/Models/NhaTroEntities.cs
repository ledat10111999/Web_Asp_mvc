namespace Do_an_Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class NhaTroEntities : DbContext
    {
        public NhaTroEntities()
            : base("name=NhaTroEntities")
        {
        }

        public virtual DbSet<district> districts { get; set; }
        public virtual DbSet<img> imgs { get; set; }
        public virtual DbSet<post> posts { get; set; }
        public virtual DbSet<project> projects { get; set; }
        public virtual DbSet<province> provinces { get; set; }
        public virtual DbSet<savepost> saveposts { get; set; }
        public virtual DbSet<street> streets { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<test> tests { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<ward> wards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<post>()
                .HasMany(e => e.imgs)
                .WithOptional(e => e.post)
                .HasForeignKey(e => new { e.IDpost, e.IDimg })
                .WillCascadeOnDelete();

            modelBuilder.Entity<test>()
                .Property(e => e.ten)
                .IsFixedLength();

            modelBuilder.Entity<user>()
                .Property(e => e.money)
                .HasPrecision(15, 2);

            modelBuilder.Entity<user>()
                .Property(e => e.Created_at)
                .HasPrecision(0);

            modelBuilder.Entity<user>()
                .Property(e => e.Update_at)
                .HasPrecision(0);

            modelBuilder.Entity<user>()
                .HasMany(e => e.posts)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.IDusers);

            modelBuilder.Entity<user>()
                .HasMany(e => e.saveposts)
                .WithRequired(e => e.user)
                .HasForeignKey(e => e.IDusers)
                .WillCascadeOnDelete(false);
        }
    }
}
