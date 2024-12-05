using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Data.Entities
{
    public class Category
    {
        public int CategId {  get; set; }
        public string CategName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
