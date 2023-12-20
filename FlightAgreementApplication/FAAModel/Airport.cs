using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public class Airport
    {
        [Key]
        public Guid AirportId { get; set; }
        public string AirportName { get; set; }
        public string AirportCode { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }    
        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public Airport()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
