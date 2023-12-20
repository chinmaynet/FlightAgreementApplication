using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class AnnexQuotation
    {
        [Key]
        public Guid AnnexQuotationID { get; set; }


        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }

        [ForeignKey("AirlineID")]
        public Guid AirlineID { get; set; }
        public Airline Airline { get; set; }      

        public string QuotationDetails { get; set; }    

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AnnexQuotation()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
