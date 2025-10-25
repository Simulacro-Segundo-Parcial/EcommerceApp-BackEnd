using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.DTOs.Cart
{
    public class RemoveFromCartDto
    {
        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, 1000)]
        public int Quantity { get; set; }
    }
}