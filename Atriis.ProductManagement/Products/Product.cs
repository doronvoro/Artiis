namespace Atriis.ProductManagement.BL
{
    public class Product
    {
        public int Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }


    public class ProductDetail
    {
        public int Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string[] Images { get; set; }
    }

}