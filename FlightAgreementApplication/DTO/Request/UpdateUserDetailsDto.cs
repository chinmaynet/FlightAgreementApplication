using FlightAgreementApplication.Model;

namespace FlightAgreementApplication.DTO.Request
{
    public class UpdateUserDetailsDto
    {
        public string UserName { get; set; }
        //public string UserEmail { get; set; }

        //public string AirlineManagerName { get; set; }
        public string? AirlineManagerPhone { get; set; }
        public string? AirlineManagerLandLine { get; set; }

        //public string TourOperatorName { get; set; }
        public string? TourOperatorAddress { get; set; }
        public string? TourOperatorPhone { get; set; }

        public string? TourOperatorLandLine { get; set; }

        public ContactPreference TourOperatorContactPreference { get; set; }

    }
}
