using Microsoft.AspNetCore.Mvc.RazorPages;
using TechChallenge.Dtos.Product;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class AddProductModel : PageModel
{
    [BindProperty]
    public ProductPostDto ProductPost { get; set; }

    public async Task OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
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
}
