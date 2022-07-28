namespace SampleWebAPI.DTO
{
    public class AddSwordToExistingElementDTO
    {
        public string SwordName { get; set; }
        public double Weight { get; set; }
        public List<SwordToExistingElementDTO> Element { get; set; }
    }
}
