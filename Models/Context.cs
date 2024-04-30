using System.Data.Entity;
using Deneme3.Models;

namespace Deneme3.Models


{
    public class Context : DbContext
    {
        public Context() : base("Data Source=tcp:yazlab1.database.windows.net,1433;Initial Catalog=yazlab2;User Id=yazlabadmin@yazlab1;Password=yazlab1*")
        {

        }
        public DbSet<Kisi> Kisis { get; set; }
        public DbSet<Danisan> Danisans { get; set; }
        public DbSet<Antrenor> Antrenors { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Mesajlar> Mesajlars { get; set; }
        public DbSet<IlerlemeKayit> IlerlemeKayits { get; set; }
        public DbSet<Danisan_Antrenor> Danisan_Antrenors { get; set; }
        public DbSet<Plan> Plans { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Kisi>().ToTable("Kisis");
            modelBuilder.Entity<Danisan>().ToTable("Danisans");
            modelBuilder.Entity<Antrenor>().ToTable("Antrenors");
            modelBuilder.Entity<Admin>().ToTable("Admins");
            modelBuilder.Entity<Mesajlar>().ToTable("Mesajlars");
            modelBuilder.Entity<Plan>().ToTable("Plans");
            modelBuilder.Entity<IlerlemeKayit>().ToTable("IlerlemeKayit");
            modelBuilder.Entity<Danisan_Antrenor>().ToTable("Danisan_Antrenors");


            /*modelBuilder.Entity<Antrenor>()
                .HasRequired(a => a.Kisi)
                .WithMany()
                .HasForeignKey(a => a.KisiID);
            modelBuilder.Entity<Danisan>()
            .HasRequired(d => d.Kisi)
            .WithMany()
            .HasForeignKey(d => d.KisiID);*/

        }
    }
}
