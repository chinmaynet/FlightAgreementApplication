using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class AnnexQuotationFlights
    {
        [Key]
        public Guid AnnexQuotationFlightsID { get; set; }


        [ForeignKey("AnnexQuotationID")]
        public Guid AnnexQuotationID { get; set; }
        public AnnexQuotation AnnexQuotation { get; set; }

        [ForeignKey("FlightID")]
        public Guid FlightID { get; set; }
        public Flight Flight { get; set; }

        [ForeignKey("AirlineID")]
        public Guid AirlineID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }

        public AnnexRequest AnnexRequest { get; set; }

      
        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AnnexQuotationFlights()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
