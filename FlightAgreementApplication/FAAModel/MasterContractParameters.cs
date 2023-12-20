using System.ComponentModel.DataAnnotations;

namespace FlightAgreementApplication.Model
{
    public class MasterContractParameters
    {
        [Key]
        public Guid MasterContractParameterID { get; set; }
        public string ParameterName { get; set; }
        public string DefaultValue { get; set; }
        public string ParameterDescription { get; set; }
        public string DataType { get; set; }


        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public MasterContractParameters()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
