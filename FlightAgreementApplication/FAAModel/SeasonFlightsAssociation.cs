using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{   
    public class SeasonFlightsAssociation
    {
        [Key]
        public Guid AssociationID { get; set; }
        public Guid SeasonID { get; set; }
        [ForeignKey("SeasonID")]
        public Season Season { get; set; }
        public Guid FlightID { get; set; }
        [ForeignKey("FlightID")]
        public Flight Flight { get; set; }  
        
        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public SeasonFlightsAssociation()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
