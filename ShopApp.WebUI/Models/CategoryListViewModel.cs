using ShopApp.Entities.Entities;
using System.Collections.Generic;


namespace ShopApp.WebUI.Models
{
    public class CategoryListViewModel
    {
        public string SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }
    }
}
