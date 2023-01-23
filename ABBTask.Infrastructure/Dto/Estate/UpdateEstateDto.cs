namespace ABBTask.Infrastructure.Dto.Estate
{
    public class UpdateEstateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsSold { get; set; }
    }
}