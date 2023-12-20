using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public enum FlightType
    {
        Virtual,
        Real
    }
    public class Flight
    {
        public Guid FlightID { get; set; }
        public Guid AirlineID { get; set; }
        [ForeignKey("AirlineID")]
        public Airline Airline { get; set; }
        public string FlightName { get; set; }
        public string FlightNumber { get; set; }
        public string FlightCode { get; set; }
        public FlightType FlightType { get; set; }
        public DateTime DepartureDate { get; set; }

        //public string DepartureAirport { get; set; }             
        public Guid DepartureAirportId { get; set; }
        [ForeignKey("DepartureAirportId")]
        public Airport DepartureAirport { get; set; }
        //public string DestinationAirport { get; set; }
        public Guid DestinationAirportId { get; set; }
        [ForeignKey("DestinationAirportId")]
        public Airport DestinationAirport { get; set; }
        
        public int PremiumSeatsNo { get; set; }
        public int EconomySeatsNo { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public Flight()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
