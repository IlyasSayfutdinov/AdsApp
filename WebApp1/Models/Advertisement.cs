using System;

namespace WebApp1.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
