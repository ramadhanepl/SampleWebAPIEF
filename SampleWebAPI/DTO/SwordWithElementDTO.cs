namespace SampleWebAPI.DTO
{
    public class SwordWithElementDTO
    {
        //public int Id { get; set; }
        public string SwordName { get; set; }
        //public double Weight { get; set; }
        public List<ElementReadDTO> Elements { get; set; }
        public SwordTypeReadDTO SwordType { get; set; }


    }
}
