using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class Tariffs
    {
        [Key]
        public Guid TariffID { get; set; }

        [ForeignKey("FlightSegmentID")]
        public Guid FlightSegmentID { get; set; }
        public FlightSegment FlightSegment { get; set; }    

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }

        public string TariffType { get; set; }
        public string TariffCode { get; set; }
        public string SSRIncluded { get; set; }
        public decimal Surcharge { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public int MIN { get; set; }
        public decimal CHDDiscount { get; set; }
        public decimal INDiscount { get; set; }
        public bool IsBlockSpace { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public Tariffs()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
