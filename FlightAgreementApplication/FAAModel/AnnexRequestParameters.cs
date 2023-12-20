using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class AnnexRequestParameter
    {
        [Key]
        public Guid AnnexRequestParameterID { get; set; }

        [ForeignKey("AnnexRequestID")]
        public Guid AnnexRequestID { get; set; }
        public AnnexRequest AnnexRequest { get; set; }  

        [ForeignKey("MasterContractParameterID")]
        public Guid MasterContractParameterID { get; set; }
        public MasterContractParameters MasterContractParameters { get; set; }  

        public string ParameterValue { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public AnnexRequestParameter()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }

}
