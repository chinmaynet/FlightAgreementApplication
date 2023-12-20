
using FlightAgreementApplication.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using static System.Reflection.Metadata.BlobBuilder;

namespace FlightAgreementApplication.Data
{
    public class FlightAgreementAppContext : DbContext

    {
        public FlightAgreementAppContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<TourOperator> TourOperators { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<SeasonFlightsAssociation> SeasonFlightsAssociations { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airport { get; set; }
        public DbSet<FlightContract> FlightContract { get; set; }
        public DbSet<AnnexFlights> AnnexFlights { get; set; }
        public DbSet<AnnexQuotation> AnnexQuotation { get; set; }
        public DbSet<AnnexQuotationFlights> AnnexQuotationFlights { get; set; }
        public DbSet<AnnexRequest> AnnexRequest { get; set; }
        public DbSet<AnnexRequestParameter> AnnexRequestParameter { get; set; }
        public DbSet<AnnexRequestSpecialServices> AnnexRequestSpecialServices { get; set; }
        public DbSet<AnnexTaxes> AnnexTaxes { get; set; }
        public DbSet<FlightSegment> FlightSegment { get; set; }
        public DbSet<GeneralTaxes> GeneralTaxes { get; set; }
        public DbSet<MasterContract> MasterContract { get; set; }
        public DbSet<MasterContractParameters> MasterContractParameters { get; set; }
        public DbSet<ReleaseFlightSettings> ReleaseFlightSettings { get; set; }
        public DbSet<ReleaseSettings> ReleaseSettings { get; set; }
        public DbSet<SpecialServices> SpecialServices { get; set; }
        public DbSet<Tariffs> Tariffs { get; set; }
        public DbSet<AirlineManager> AirlineManagers { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRole { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=.;Database=BookStroreAPI;Integrated Security=True;TrustServerCertificate=True"); //to estabilish Communication with db cs is mandatory  //connection string
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<SeasonFlightsAssociation>()
                .HasKey(sfa => sfa.AssociationID);

            modelBuilder.Entity<SeasonFlightsAssociation>()
                .HasOne(sfa => sfa.Season)
                .WithMany()
                .HasForeignKey(sfa => sfa.SeasonID);

            modelBuilder.Entity<SeasonFlightsAssociation>()
                .HasOne(sfa => sfa.Flight)
                .WithMany()
                .HasForeignKey(sfa => sfa.FlightID);


            modelBuilder.Entity<Flight>()
                .HasKey(sfa => sfa.FlightID);

            modelBuilder.Entity<Flight>()
                .HasOne(sfa => sfa.Airline)
                .WithMany()
                .HasForeignKey(sfa => sfa.AirlineID);

            //decimal issue 

            modelBuilder.Entity<GeneralTaxes>()
        .Property(gt => gt.Amount)
        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SpecialServices>()
                .Property(ss => ss.Surcharge)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tariffs>()
                .Property(t => t.CHDDiscount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tariffs>()
                .Property(t => t.INDiscount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Tariffs>()
                .Property(t => t.Surcharge)
                .HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
        }

    }
}
