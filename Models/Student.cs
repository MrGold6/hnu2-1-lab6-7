using Azure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace LabW4.Models
{
    public class Student
    {
        public string StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Date_of_admission { get; set; }
        public bool IsGraduated { get; set; }
        public string Atestat { get; set; }
        public int NumbeOfRecordBook { get; set; }
        public string Document { get; set; }

        public string Group { get; set; }
        public string Speciality { get; set; }
        public int Course { get; set; }


        public int VoucherId { get; set; }
        public virtual  Voucher Voucher { get; set; }

        public string UniversityId { get; set; } // Required foreign key property
        public virtual  University University { get; set; }

        public List<TradeUnion> TradeUnions { get;set; } = [];


    }
}
