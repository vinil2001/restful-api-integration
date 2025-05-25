using Microsoft.AspNetCore.Mvc;
using RestfullApiIntegration.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestfullApiIntegration.ApiControllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private const string MockApiUrl = "https://restful-api.dev/";

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. Retrieve data with filtering and paging
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            if (page <= 0 || size <= 0)
            {
                ModelState.AddModelError("Paging", "Page and size must be positive integers.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _httpClient.GetAsync($"{MockApiUrl}objects");
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            var products = JsonSerializer.Deserialize<List<Product>>(data);

            var filteredProducts = products
                .Where(p => string.IsNullOrEmpty(name) || p.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

            return Ok(filteredProducts);
        }


        // 2. Add data to the mock API
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jsonContent = new StringContent(JsonSerializer.Serialize(product), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{MockApiUrl}objects", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to add product.");
            }

            return Created("", product);
        }

        // 3. Remove data via the mock API
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                ModelState.AddModelError("Id", "Product ID is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _httpClient.DeleteAsync($"{MockApiUrl}objects/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return NoContent();
        }

    }

}
