using SampleWebAPI.Domain;

namespace SampleWebAPI.DTO
{
    public class AddSamuraiWithSwordDTO
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public List<SwordCreateDTO> Swords { get; set; }
  
    }
}
