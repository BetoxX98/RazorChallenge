using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechChallenge.Pages
{
    public class ProductTypeModel : PageModel
    {

        public ProductTypeModel()
        {
        }

        //public async Task OnGet()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7050/api/products");
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var stringResponse = await response.Content.ReadAsStringAsync();
        //            var productsResponse = JsonSerializer.Deserialize<ApiResponseDto<List<ProductDto>>>(stringResponse);
        //            if (productsResponse != null && productsResponse.Success)
        //            {
        //                Products = productsResponse.Data;
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}
    }

}
