using Microsoft.AspNetCore.Mvc.RazorPages;
using TechChallenge.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TechChallenge.Dtos.ProductType;

public class AddProductModel : PageModel
{
    [BindProperty]
    public ProductPostDto ProductPost { get; set; }

    public List<ProductTypeDto> ProductTypes = new List<ProductTypeDto>();
    public async Task OnGet()
    {
        try
        {
            var productTypesListJson = TempData["ProductTypes"].ToString();
            ProductTypes = JsonSerializer.Deserialize<List<ProductTypeDto>>(productTypesListJson);
        }
        catch (Exception)
        {  }
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7050/api/products");
            var content = new StringContent(JsonSerializer.Serialize(ProductPost), System.Text.Encoding.UTF8, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Products");
            }

            return Page();
        }
        catch (Exception)
        { return Page(); }
    }
}
