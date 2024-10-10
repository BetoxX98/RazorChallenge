using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.Product;
using TechChallenge.Dtos.ProductType;

namespace TechChallenge.Pages
{
    public class ProductsModel : PageModel
    {
        public ProductsModel()
        {
        }

        [BindProperty]
        public List<ProductDto> Products { get; set; }
        [BindProperty]
        public List<ProductTypeDto> ProductTypes { get; set; } 
        [BindProperty]
        public Guid ProductId { get; set; }

        public async Task OnGet()
        {
            try
            {
                //GET PRODUCTS
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

                        //GET PRODUCT TYPES
                        request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7050/api/product-types");
                        response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            stringResponse = await response.Content.ReadAsStringAsync();
                            var productTypesResponse = JsonSerializer.Deserialize<ApiResponseDto<List<ProductTypeDto>>>(stringResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            if (productTypesResponse != null && productTypesResponse.Success)
                            {
                                ProductTypes = productTypesResponse.Data;
                                TempData["ProductTypes"] = JsonSerializer.Serialize(ProductTypes);
                            }
                        }
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
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7050/api/products/{ProductId}");
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
