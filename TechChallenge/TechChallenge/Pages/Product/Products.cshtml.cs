using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.Product;

namespace TechChallenge.Pages
{
    public class ProductsModel : PageModel
    {
        public ProductsModel()
        {
        }

        public List<ProductDto> Products { get; set; } = new List<ProductDto>();

        public async Task OnGet()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7050/api/products");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var productsResponse = JsonSerializer.Deserialize<ApiResponseDto<List<ProductDto>>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (productsResponse != null && productsResponse.Success)
                    {
                        Products = productsResponse.Data;
                    }
                }
            }
            catch (Exception)
            { }
        }

    }

}
