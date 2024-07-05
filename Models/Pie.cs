using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace BethanysPieShop.Models
{
    public class Pie
    {
        public int PieId { get; set; }

        [Required(ErrorMessage = "Please enter the pie name")]
        [Display(Name = "Pie Name")]
        public string Name { get; set; } = string.Empty;
        public string? ShortDescription { get; set; }
        public string? LongDescription { get; set; }
        public string? AllergyInformation { get; set; }

        [Required(ErrorMessage = "Please enter the price in $:")]
        [Display(Name = "Price in $")]
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }

        [Display(Name = "Is Pie of the Week?")]
        public bool IsPieOfTheWeek { get; set; }
        [Display(Name = "In Stocks?")]
        public bool InStock { get; set; }

        [Required(ErrorMessage = "Select Category ID")]
        public int CategoryId { get; set; }
        // public Category Category { get; set; } = default!;
        public Category? Category { get; set; }
    }
}
