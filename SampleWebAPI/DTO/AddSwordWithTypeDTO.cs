namespace SampleWebAPI.DTO
{
    public class AddSwordWithTypeDTO
    {
        public string SwordName { get; set; }
        public double Weight { get; set; }
        public AddSwordTypeDTO SwordType { get; set; }
    }
}
