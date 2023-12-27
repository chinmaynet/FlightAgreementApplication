using FlightAgreementApplication.Model;

namespace FlightAgreementApplication.DTO.Response
{
    public class AirlineManagerOperationsResponse
    {
        public class TourOperatorResponse{


        }

        public class GetAllTourOperatorsResponse {
            public int TotalRows { get; set; }
            public List<TourOperator> TourOperators { get; set; }
           
        }
    }
}
