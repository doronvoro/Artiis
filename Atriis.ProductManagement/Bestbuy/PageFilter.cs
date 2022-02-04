using System.ComponentModel.DataAnnotations;

namespace Atriis.ProductManagement.BL
{
    public class PageFilter
    {
        public string? TextToSearch { get; set; } = string.Empty;
      
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}