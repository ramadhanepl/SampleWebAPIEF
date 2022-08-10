namespace SampleWebAPI.DTO
{
    public class AddElementToExistingSwordDTO
    {
        public int Id { get; set; }
        public List<ExistingSwordElementDTO> Elements { get; set; }
    }
}
