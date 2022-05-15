using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAMS.Application.Interfaces.Persistence;
using PAMS.Domain.Entities;
using PAMS.Domain.Entities.FieldScientistEntities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PAMS.Persistence.Context
{
    public class PAMSdbContext : IdentityDbContext<PamsUser>, IPAMSdbContext
    {
        public PAMSdbContext(DbContextOptions<PAMSdbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        #region Tables
        public DbSet<PamsUser> PamsUsers { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Sampling> Samplings { get; set; }
        public DbSet<ClientSample> ClientSamples { get; set; }
        public DbSet<Inventory>  Inventories { get; set; }
        public DbSet<Invoice>  Invoices { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<PhysicoChemicalAnalysis> PhysicoChemicalAnalyses{ get; set; }
        public DbSet<MicroBiologicalAnalysis> MicroBiologicalAnalyses{ get; set; }
        public DbSet<Report> Reports{ get; set; }
        public DbSet<EquipmentInventory> EquipmentInventories{ get; set; }
        public DbSet<MicroBiologicalAnalysisTemplate> MicroBiologicalAnalysisTemplates{ get; set; }
        public DbSet<NESREA>    NESREAs { get; set; }
        public DbSet<NESREAParameterTemplate>     NESREAParameters { get; set; }
        public DbSet<DPR>   DPRs { get; set; }
        public DbSet<DPRParameterTemplate>   DPRParameters { get; set; }
        public DbSet<FMEnv>  FMVEnvs { get; set; }
        public DbSet<FMEnvParameterTemplate>   FMEnvParameterTemplates { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AirQualityTemplate> AirQualityTemplates { get; set; }
        public DbSet<AirQualityParameter> AirQualityParameters { get; set; }
        public DbSet<AirQualitySample> AirQualitySamples { get; set; }
        public DbSet<AirQualitySampleParameter> AirQualitySampleParameters { get; set; }
        
        public DbSet<SamplePointLocation> SamplePointLocation { get; set; }
        public DbSet<FieldLocation> FieldLocations { get; set; }

        public DbSet<NESREAField> NESREAFields { get; set; }
        public DbSet<NESREATemplate> NESREATemplate { get; set; }
        public DbSet<NESREAFieldResult> NESREAFieldResult { get; set; }
        
        public DbSet<FMENVField> FMENVFields { get; set; }
        public DbSet<FMENVTemplate> FMENVTemplate { get; set; }
        public DbSet<FMENVFieldResult> FMENVFieldResult { get; set; }

        public DbSet<DPRField> DPRFields { get; set; }
        public DbSet<DPRTemplate> DPRTemplate { get; set; }
        public DbSet<DPRFieldResult> DPRFieldResult { get; set; }

        public DbSet<ImageModel> ImageModels { get; set; }


        #endregion Tables

        /// <summary>
        /// This method saves changes made to context, once called upon.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(modelBuilder);
        }
    }
}