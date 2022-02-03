namespace Atriis.ProductManagement.BL
{
    public class PageFilter
    {
        public string? TextToSearch { get; set; } = string.Empty;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}