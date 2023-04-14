namespace HPlusSport.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Relationship: Category hasMany products
        public virtual List<Product> Products { get; set; } = new List<Product>();
    }
}