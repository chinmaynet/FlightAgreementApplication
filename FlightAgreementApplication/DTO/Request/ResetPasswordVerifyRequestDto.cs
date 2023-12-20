namespace FlightAgreementApplication.DTO.Request
{
    public class ResetPasswordVerifyRequestDto
    {
        public string ResetToken { get; set; }
        public string UserEmail { get; set; }
        public string NewPassword { get; set; }
    }
}
