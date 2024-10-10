using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.Product;
using TechChallenge.Dtos.ProductType;

namespace TechChallenge.Pages
{
    public class ProductTypeModel : PageModel
    {

        public ProductTypeModel()
        {
        }

        public List<ProductTypeDto> ProductTypes { get; set; } = new List<ProductTypeDto>();
        [BindProperty]
        public Guid ProductTypeId { get; set; }

        public async Task OnGet()
        {
            try
            {
                //GET PRODUCT TYPES
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7050/api/product-types");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    var productTypesResponse = JsonSerializer.Deserialize<ApiResponseDto<List<ProductTypeDto>>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (productTypesResponse != null && productTypesResponse.Success)
                    {
                        ProductTypes = productTypesResponse.Data;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7050/api/product-types/{ProductTypeId}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage();
                }
            }
            catch (Exception)
            { return RedirectToPage(); }

            return RedirectToPage();

        }
    }

}
