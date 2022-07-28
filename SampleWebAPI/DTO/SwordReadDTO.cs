namespace SampleWebAPI.DTO
{
    public class SwordReadDTO
    {
        public int Id { get; set; }
        public string SwordName { get; set; }
        public double Weight { get; set; }
        public SwordTypeReadDTO SwordType { get; set; }


    }
}
