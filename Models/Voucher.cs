using System.ComponentModel.DataAnnotations;

namespace LabW4.Models
{
    public class Voucher
    {
        public int VoucherId { get; set; }
        public int Code { get; set; }
        public string Insuranse { get; set; }
        public bool HasSpecialStaff { get; set; }
        public string Transport { get; set; }
        public string City { get; set; }

        public string Country { get; set; }
        public string Date { get; set; }
        public int Cost { get; set; }

    }
}
