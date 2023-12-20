using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class ReleaseSettings
    {
        [Key]
        public Guid ReleaseSettingsID { get; set; }

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }


        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public ReleaseSettings()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
