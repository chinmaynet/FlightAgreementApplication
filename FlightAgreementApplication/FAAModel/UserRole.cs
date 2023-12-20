using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace FlightAgreementApplication.Model
{
    public class UserRole
    {
        public Guid UserRoleId { get; set; }

        public Guid UId { get; set; }
        [ForeignKey("UId")]
        public User User { get; set; }

        public Guid RId { get; set; }
        [ForeignKey("RId")]
        public Role Role { get; set; }

        [AllowNull]
        public Guid? TourOperatorId { get; set; }

        [ForeignKey("TourOperatorId")]
        public TourOperator? TourOperator { get; set; }

        [AllowNull]
        public Guid? AirlineManagerId { get; set; }

        [ForeignKey("AirlineManagerId")]
        public AirlineManager? AirlineManager { get; set; }
    }
}
