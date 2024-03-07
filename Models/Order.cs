using System;
using System.ComponentModel.DataAnnotations;

namespace ITStepShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        public DateTime OrderDate { get; set; }

        // Додайте інші необхідні вам поля
    }
}
