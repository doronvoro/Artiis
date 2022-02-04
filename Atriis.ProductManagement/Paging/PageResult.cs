namespace Atriis.ProductManagement.BL
{
    public class PageResult<T>
    {
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public List<T> Items { get; set; }
    }
}