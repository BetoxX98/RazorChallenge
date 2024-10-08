using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using TechChallenge.Dtos.Product;
using System.Text.Json;

public class EditProductModel : PageModel
{
    [BindProperty]
    public ProductPutDto ProductPut { get; set; }

    public async Task OnGet(int id)
    {
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync($"https://localhost:7050/api/products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                ProductPut = JsonSerializer.Deserialize<ProductPutDto>(jsonString);
            }
        }
    }

    public async Task<IActionResult> OnPost()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Put, "https://localhost:7050/api/products");
        var content = new StringContent(JsonSerializer.Serialize(ProductPut), System.Text.Encoding.UTF8, "application/json");
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
