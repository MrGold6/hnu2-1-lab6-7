namespace LabW4.Models
{
    public class TradeUnion
    {
        public int TradeUnionId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public string DateOfCreation { get; set; }

        public string Document { get; set; }
        public bool IsApproved { get; set; }


        public List<Student> Students { get; set; } = [];



    }
}
