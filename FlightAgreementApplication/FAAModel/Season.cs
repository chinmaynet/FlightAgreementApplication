using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
   
    public enum SeasonStatus
    {
        Active,
        Inactive,
        Upcoming
    }

    public class Season
    {
        [Key]
        public Guid SeasonID { get; set; }
        public string SeasonCode { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SeasonStatus Status { get; set; }

        public IsDeleted DeletedStatus { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public Season()
        {
            AddedDate = DateTime.UtcNow;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
    
}
