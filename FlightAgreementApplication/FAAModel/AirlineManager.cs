using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public class AirlineManager
    {
        [Key]
        public Guid AirlineManagerId { get; set; }
        public string AirlineManagerName { get; set; }

        public string AirlineManagerEmail { get; set; }
        public string AirlineManagerPhone { get; set; }

        public string AirlineManagerLandLine { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        //Automatically fill the AddedBy property with the ID or name of the airline manager in the controller when creating a new TourOperator instance.
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AirlineManager()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
