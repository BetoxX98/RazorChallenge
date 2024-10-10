using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using TechChallenge.Dtos.Product;
using TechChallenge.Dtos.ProductType;

namespace TechChallenge.Pages.ProductType
{
    public class AddProductTypeModel : PageModel
    {
        [BindProperty]
        public ProductTypePostDto ProductTypePost { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7050/api/product-types");
                var content = new StringContent(JsonSerializer.Serialize(ProductTypePost), System.Text.Encoding.UTF8, "application/json");
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
