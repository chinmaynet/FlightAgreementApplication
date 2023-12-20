namespace FlightAgreementApplication.Dto
{
    public class UpdatePasswordDto
    {
        public string UserEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
