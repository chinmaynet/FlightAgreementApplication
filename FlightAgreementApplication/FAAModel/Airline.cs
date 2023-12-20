using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public class Airline
    {
        [Key]
        public Guid AirlineID { get; set; }
        public string AirlineName { get; set; }
        public string AirlineEmail { get; set; }
        public string AirlinePhone { get; set; }
        public string AirlineLandLine { get; set; }

        public ContactPreference AirlineContactPreferences { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public Airline()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
