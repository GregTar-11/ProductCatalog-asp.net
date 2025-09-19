namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public int IdProduct{ get; set; }       // идентификатор
        public string Name { get; set; } = string.Empty;  // название
        public decimal Price { get; set; } // цена
    }
}
