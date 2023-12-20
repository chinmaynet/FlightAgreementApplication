using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class AnnexRequestSpecialServices
    {
        [Key]
        public Guid AnnexRequestSpecialServicesID { get; set; }

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }

        [ForeignKey("SSID")]
        public Guid SSID { get; set; }
        public SpecialServices SpecialServices { get; set; }

        [ForeignKey("FlightSegmentID")]
        public Guid FlightSegmentID { get; set; }
        public FlightSegment FlightSegment { get; set; }


        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AnnexRequestSpecialServices()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
