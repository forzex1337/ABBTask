namespace ABBTask.Infrastructure.Models.Estate
{
    public class EstateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; } 
        public DateTime ModificationDate { get; set; }
        public bool IsSold { get; set; }
    }
}