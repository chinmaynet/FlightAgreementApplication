namespace FlightAgreementApplication.DTO.Request
{
    public class LoginRequestDto
    {
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
    public class ResetPasswordRequest
    {

        public string UserEmail { get; set; }
    }
   
}
