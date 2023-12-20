using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public class SpecialServices
    {
        [Key]
        public Guid SSID { get; set; }
        public string SSRCode { get; set; }
        public string SSRName { get; set; }
        public decimal Surcharge { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public SpecialServices()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
