using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.ProductType;

namespace TechChallenge.Pages.ProductType
{
    public class EditProductTypeModel : PageModel
    {
        [BindProperty]
        public ProductTypePutDto ProductTypePut { get; set; }

        public async Task OnGet(Guid id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7050/api/product-types/{id.ToString()}");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var res = JsonSerializer.Deserialize<ApiResponseDto<ProductTypeDto>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (res.Success)
                    {
                        ProductTypePut = new ProductTypePutDto()
                        {
                            Id = res.Data.Id,
                            Name = res.Data.Name,
                            Description = res.Data.Description,
                            Color = res.Data.Color,
                        };
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
                var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7050/api/product-types");
                var content = new StringContent(JsonSerializer.Serialize(ProductTypePut), System.Text.Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("ProductTypes");
                }

                return Page();
            }
            catch (Exception)
            { return Page(); }
        }
    }
}
