using CoronaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaWeb.CoronaDb
{
    public class CoronaDbContext : DbContext
    {
        public CoronaDbContext(DbContextOptions<CoronaDbContext> options) : base(options)
        {

        }
        public DbSet<NewsModel> NewsModels { get; set; }
        public DbSet<NewsSourceModel>  NewsSourceModels { get; set; }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<DiseaseModel> DiseaseModels { get; set; }
        public DbSet <MedicalStateModel>  MedicalStateModels{ get; set; }
        public DbSet<CountryModel>  CountryModels { get; set; }
        public DbSet<StatisticsModel>  StatisticsModels { get; set; }
        public DbSet<SOSModel> SOSModels { get; set; }
        public DbSet<LocationModel>  LocationModels { get; set; }
        public CoronaDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=SQL5031.site4now.net;Initial Catalog=DB_A550E5_ranaashrafe27;User Id=DB_A550E5_ranaashrafe27_admin;Password=rana123456;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
