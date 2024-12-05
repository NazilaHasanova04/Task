namespace ProductsAndCategories.Data.Entities
{
    public class Product
    {
        public int ProdId { get; set; }
        public string ProdName {  get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
