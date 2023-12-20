using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public enum ContactPreference
    {
        Email = 0,
        Phone = 1,
        Landline = 2
    }
    public enum IsActive
    {
        Active,
        Inactive
    }
    public enum IsDeleted
    {
        NotDeleted,
        Deleted
    }
    public class TourOperator
    {
        [Key]
        public Guid TourOperatorId { get; set; }
        public string TourOperatorName { get; set; }
        public string TourOperatorAddress { get; set; }
        public string TourOperatorEmail { get; set; }
        public string TourOperatorPhone { get; set; }

        public string TourOperatorLandLine { get; set; }       
        public ContactPreference TourOperatorContactPreferences { get; set; }
        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        //Automatically fill the AddedBy property with the ID or name of the airline manager in the controller when creating a new TourOperator instance.
        public DateTime AddedDate { get;set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }
        
        public TourOperator()
        {            
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
