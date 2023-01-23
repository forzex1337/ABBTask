namespace ABBTask.Infrastructure.Dto.Estate
{
    public class CreateEstateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; } 
        public bool IsSold { get; set; }
    }
}