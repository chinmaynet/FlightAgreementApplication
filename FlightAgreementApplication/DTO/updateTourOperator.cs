using FlightAgreementApplication.Model;

namespace FlightAgreementApplication.DTO
{
    public class updateTourOperator
    {
        public string TourOperatorName { get; set; }
        public string TourOperatorAddress { get; set; }
        public string TourOperatorEmail { get; set; }
        public string TourOperatorPhone { get; set; }

        public string TourOperatorLandLine { get; set; }
        public ContactPreference TourOperatorContactPreferences { get; set; }
    }
}
