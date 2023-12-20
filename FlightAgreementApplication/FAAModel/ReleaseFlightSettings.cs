using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class ReleaseFlightSettings
    {
        public Guid ReleaseFlightSettingsId { get; set; }

        [ForeignKey("ReleaseSettingsID")]
        public Guid ReleaseSettingsID { get; set; }
        public ReleaseSettings ReleaseSettings { get; set; }

        [ForeignKey("FlightID")]
        public Guid FlightID { get; set; }
        public Flight Flight { get; set; }

        public string Days { get; set; }
        public string ReleaseSeats { get; set; } 

      


        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public ReleaseFlightSettings()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
