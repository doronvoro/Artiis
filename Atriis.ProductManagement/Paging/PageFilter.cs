using System.ComponentModel.DataAnnotations;

namespace Atriis.ProductManagement.BL
{
    public class PageFilter
    {
        public string? TextToSearch { get; set; } = string.Empty;
      
       // todo: [Required]
        public int PageIndex { get; set; }
        // todo: [Required]
        public int PageSize { get; set; }

        public string? SortCoulmn { get; set; } = string.Empty;
    }
}