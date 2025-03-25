using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mottrist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {


        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    // Send GET request to the external API URL
                    HttpResponseMessage response = await httpClient.GetAsync("https://countriesnow.space/api/v0.1/countries");
                    response.EnsureSuccessStatusCode();

                    // Read the JSON response as a string
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    return Ok(jsonResult);
                }
                catch (HttpRequestException ex)
                {
                    // Return a 500 error with the exception message if an error occurs
                    return StatusCode(500, $"Error retrieving data: {ex.Message}");
                }
            }
        }
    }
}
