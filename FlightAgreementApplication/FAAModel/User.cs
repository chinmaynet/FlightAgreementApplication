using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        //public string UserLastName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }       

        [DefaultValue("Inactive")]                  //
        public IsActive ActivityStatus { get; set; }

        // Activation token properties
        public string? ActivationToken { get; set; } //
        public DateTime? ActivationTokenExpiry { get; set; } //

        // Password reset token properties
        public string? ResetToken { get; set; }  //
        public DateTime? ResetTokenExpiry { get; set; }  //

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
