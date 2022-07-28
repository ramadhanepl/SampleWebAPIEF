namespace SampleWebAPI.DTO
{
    public class SamuraiWithFullAttributeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordWithElementDTO> Swords { get; set; } 
    }
}
