using GothamCity.Domain.Entities;
using GothamCity.Infra.Repository.Map;
using Microsoft.EntityFrameworkCore;

namespace GothamCity.Infra.Repository.Base
{
    public partial class GothamCityContext : DbContext
    {
        public DbSet<Cep> Cep { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=gothamcity;Uid=root;Pwd=masterkey");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Ignore<Notification>();

            modelBuilder.ApplyConfiguration(new MapCep());

            base.OnModelCreating(modelBuilder);
        }
    }


}
