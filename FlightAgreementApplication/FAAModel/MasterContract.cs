using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace FlightAgreementApplication.Model
{
    public enum ContractTypes
    {
        MasterTourOperatorToAirline,
        OtherTourOperatorTOAirline,
        MasterTourOperatorToAnotherAirline
    }
    public enum Status
    {
        Active,
        Expired
    }
    public class MasterContract
    {
        [Key]
        public Guid MasterContractID { get; set; }

        public ContractTypes ContractType { get; set; }
        public Guid AirlineID { get; set; }
        [ForeignKey("AirlineID")]
        public Airline Airline { get; set; }

        public Guid TourOperatorId { get; set; }
        [ForeignKey("TourOperatorId")]
        public TourOperator TourOperator { get; set; }
        
        public string ContractName { get; set; }
        public enum Status
        {
            Active,
            Expired
        }
        public string TermsAndConditions { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string RenewalInfo { get; set; }
        public string ApprovalWorkflow { get; set; }
        public string TerminationClause { get; set; }
        public string ConfidentialityAndNonDisclosure { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }       

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public MasterContract()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
