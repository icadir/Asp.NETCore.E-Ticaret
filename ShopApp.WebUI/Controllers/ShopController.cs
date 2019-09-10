using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = _productService.GetProductDetails((int)id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailsModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(x => x.Category).ToList()
            });
        }
        const int pageSize = 3;
        public IActionResult List(string category, int page = 1)
        {
            return View(new ProductListModel()
            {
                Products = _productService.GetProductsByCategory(category, page, pageSize),
            });
        }
    }
}