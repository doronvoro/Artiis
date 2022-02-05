namespace Atriis.ProductManagement.BL
{
    public class BestBuyProducDetail
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public double SalePrice { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int Sku { get; set; }
        public List<BestBuyImageProduct> Images { get; set; }
    }
}