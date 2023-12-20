using FlightAgreementApplication.Model;
using System.Data;

namespace FlightAgreementApplication.DTO.Response
{
    //public class LoginResponse
    //{
    //    public Guid UserId { get; set; }
    //    public string UserName { get; set; }
    //    public string Token { get; set; }

    //    public string UserEmail { get; set; }  
    //     public string UserRole { get; set; }
    //    public string? Message { get; set; }

    //    public string? AirlineManagerPhone { get; set; }
    //    public string? AirlineManagerLandLine { get; set; }
    //    public string? TourOperatorAddress { get; set; }
    //    public string? TourOperatorPhone { get; set; }
    //    public string? TourOperatorLandLine { get; set; }
    //    public ContactPreference? TourOperatorContactPreference { get; set; }

    //}

    public class LoginResponse
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string? Message { get; set; }
        public UserDetails? Details { get; set; }
    }

    public class UserDetails
    {
        public string? AirlineManagerPhone { get; set; }
        public string? AirlineManagerLandLine { get; set; }
        public string? TourOperatorAddress { get; set; }
        public string? TourOperatorPhone { get; set; }
        public string? TourOperatorLandLine { get; set; }
        public ContactPreference? TourOperatorContactPreference { get; set; }
    }
    //public class UserDetails
    //{
    //    public string? AirlineManagerPhone { get; set; }
    //    public string? AirlineManagerLandLine { get; set; }
    //    public string? TourOperatorAddress { get; set; }
    //    public string? TourOperatorPhone { get; set; }
    //    public string? TourOperatorLandLine { get; set; }
    //    public ContactPreference? TourOperatorContactPreference { get; set; }
    //}
    public class AirlineManagerCreateResponse
    {

        public string Token { get; set; }
        public Guid AirlineManagerId { get; set; }
        public string AirlineManagerName { get; set; }

        public string AirlineManagerEmail { get; set; }
        public string AirlineManagerPhone { get; set; }

        public string AirlineManagerLandLine { get; set; }

        public string Message { get; set; }

        public string AddedBy { get; set; }
        public string UserRole { get; set; }

        public Guid UserRoleId { get; set; }
        public Guid UId { get; set; }
        public Guid RId { get; set; }

    }
    public class UpdatePasswordResponse { 
    
        public string messege { get; set; }
    
    }
    
        public class TourOperatorCreateResponse {

        public Guid TourOperatorId { get; set; }
        public string TourOperatorName { get; set; }
        public string TourOperatorAddress { get; set; }
        public string TourOperatorEmail { get; set; }
        public string TourOperatorPhone { get; set; }
        public string TourOperatorLandLine { get; set; }
        public ContactPreference TourOperatorContactPreferences { get; set; }
        public string AddedBy { get; set; }
        public Guid UserRoleId { get; set; }
        public Guid UId { get; set; }
        public Guid RId { get; set; }
        public string UserRole { get; set; }


    }
    public class ActivateAccountResponse {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string UserEmail { get; set; }
        public string UserRole { get; set; }
        public string? Message { get; set; }
        public UserDetails? Details { get; set; }
    }
 

}
