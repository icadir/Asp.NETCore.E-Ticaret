﻿using System;
using System.ComponentModel.DataAnnotations;


namespace ShopApp.WebUI.Models
{
    public class OrderModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Display(Name = "Şehir")]
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string CVV { get; set; }
        public string CartNumber { get; set; }
        public string CardName { get; set; }
        public CartModel CartModel { get; set; }

    }
}
