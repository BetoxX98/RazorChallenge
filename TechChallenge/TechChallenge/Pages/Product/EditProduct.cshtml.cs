using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechChallenge.Dtos.Product;
using System.Text.Json;
using TechChallenge.Dtos.Base;
using TechChallenge.Dtos.ProductType;
using System.Security.Cryptography.Xml;

public class EditProductModel : PageModel
{
    [BindProperty]
    public ProductPutDto ProductPut { get; set; }
    public List<ProductTypeDto> ProductTypes { get; set; } = new List<ProductTypeDto>();

    public async Task OnGet(Guid id)
    {
        try
        {
            var productTypesListJson = TempData["ProductTypes"].ToString();
            ProductTypes = JsonSerializer.Deserialize<List<ProductTypeDto>>(productTypesListJson);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7050/api/products/{id.ToString()}");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var res = JsonSerializer.Deserialize<ApiResponseDto<ProductDto>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (res.Success)
                {
                    ProductPut = new ProductPutDto() {
                        Id = res.Data.Id,
                        Name = res.Data.Name,
                        Price = res.Data.Price,
                        TypeId = res.Data.Type.Id,
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
        catch (Exception)
        { return Page(); }
    }
}
