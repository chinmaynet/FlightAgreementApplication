using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class AnnexTaxes
    {
        [Key]
        public Guid AnnexTaxID { get; set; }

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }

        [ForeignKey("AnnexFlightsID")]
        public Guid AnnexFlightsID { get; set; }
        public AnnexFlights AnnexFlights { get; set; }

        [ForeignKey("TaxID")]
        public Guid TaxID { get; set; }
        public GeneralTaxes GeneralTaxes { get; set; }
       
        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AnnexTaxes()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
