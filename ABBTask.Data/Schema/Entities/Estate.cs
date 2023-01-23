namespace ABBTask.Data.Schema.Entities
{
    public class Estate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }  // uznajmy, że data wygaśnięcia to: CreatedDate + 30 days  
        public DateTime ModificationDate { get; set; }
        public bool IsSold { get; set; }
    }
}
