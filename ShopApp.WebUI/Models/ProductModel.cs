using ShopApp.Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        //[Required]
        //[StringLength(60, MinimumLength =10,ErrorMessage ="Urun İsmi Minimum 10 karakter ve Maksimum 60 karakter olmalıdır.")]
        public string Name { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 20, ErrorMessage = "Urun İsmi Minimum 20 karakter ve Maksimum 100 karakter olmalıdır.")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Fiyat Belirtiniz.")]
        [Range(1,1000)]
        public decimal? Price { get; set; }


        public List<Category> SelectedCategories { get; set; }
    }
}
