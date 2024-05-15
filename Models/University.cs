using Microsoft.Extensions.Hosting;

namespace LabW4.Models
{
    public class University
    {
        public string UniversityId { get; set; }
        public int Count { get; set; }
        public bool IsCertified { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public static string DateOfCreation { get; set; }

        public int TradeUnionId { get; set; }
        public virtual TradeUnion TradeUnion { get; set; } = new TradeUnion();

        public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
