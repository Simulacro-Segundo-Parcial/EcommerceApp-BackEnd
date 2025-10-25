using System.Collections.Generic;

namespace EcommerceApi.DTOs.Cart
{
    public class CartResponseDto
    {
        public int UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new();
        public decimal Total { get; set; }
    }
}