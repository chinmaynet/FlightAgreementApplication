using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightAgreementApplication.Model
{
    public enum CurrencyType
    {
        USD, // United States Dollar
        EUR, // Euro
        GBP, // British Pound Sterling
        JPY, // Japanese Yen
        INR, // Indian Rupee
        AUD, // Australian Dollar
        CAD, // Canadian Dollar
        CHF, // Swiss Franc
        CNY, // Chinese Yuan
        SEK, // Swedish Krona
        NZD, // New Zealand Dollar
        SGD, // Singapore Dollar
        HKD, // Hong Kong Dollar
        NOK, // Norwegian Krone
        KRW, // South Korean Won
        TRY, // Turkish Lira
        ZAR, // South African Rand
        BRL, // Brazilian Real
        RUB, // Russian Ruble
        MXN, // Mexican Peso
        PLN, // Polish Złoty
        THB, // Thai Baht
        IDR, // Indonesian Rupiah
        MYR, // Malaysian Ringgit
        PHP, // Philippine Peso
        SAR, // Saudi Riyal
        AED, // United Arab Emirates Dirham
        QAR, // Qatari Riyal
        CLP, // Chilean Peso
        COP, // Colombian Peso
        ARS, // Argentine Peso
        DKK, // Danish Krone
        ILS, // Israeli New Shekel
        EGP, // Egyptian Pound
        HUF, // Hungarian Forint
        KWD, // Kuwaiti Dinar
        OMR, // Omani Rial     
        VND, // Vietnamese Đồng
        BHD, // Bahraini Dinar
        IQD, // Iraqi Dinar
        JOD, // Jordanian Dinar        
        LBP, // Lebanese Pound        
    }
    public enum AnnexStatus
    {
        Requested,
        Quoted,
        Confirmed,
        Rejected,
        Negotiating
    }
    public class AnnexRequest
    {
        [Key]
        public Guid AnnexRequestID { get; set; }

  
        [ForeignKey("AirlineID")]
        public Guid AirlineID { get; set; }
        public Airline Airline { get; set; }

        [ForeignKey("MasterContractID")]
        public Guid MasterContractID { get; set; }
        public MasterContract MasterContract { get; set; }

        [ForeignKey("TourOperatorID")]
        public Guid TourOperatorID { get; set; }
        public TourOperator TourOperator { get; set; }

        [ForeignKey("SeasonID")]
        public Guid SeasonID { get; set; }
        public Season Season { get; set; }

        public string AnnexTitle { get; set; }
        public CurrencyType Currency { get; set; }
        public DateTime ValidityStartDate { get; set; }
        public DateTime ValidityEndDate { get; set; }
        public string Destination { get; set; }
        public AnnexStatus RequestStatus { get; set; }
      
        public string AnnexContractDetails { get; set; }

        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifyDate { get; set; }

        public IsActive ActivityStatus { get; set; }
        public IsDeleted DeletedStatus { get; set; }

        public AnnexRequest()
        {
            AddedDate = DateTime.UtcNow;
            ActivityStatus = IsActive.Active;
            DeletedStatus = IsDeleted.NotDeleted;
        }
    }
}
