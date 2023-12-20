using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public enum ApplicabilityType
    {
        ADT,
        CHD
    }

    public enum DirectionType
    {
        IN,
        OUT
    }

    public class GeneralTaxes
    {
        [Key]
        public Guid TaxID { get; set; }

        [ForeignKey("AirportId")]
        public Guid AirportId { get; set; }
        public Airport Airport { get; set; }

        public string TaxCode { get; set; }
        public decimal Amount { get; set; }
        public ApplicabilityType Applicability { get; set; }
        public DirectionType Direction { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public bool TaxIncluded { get; set; }     

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public GeneralTaxes()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
