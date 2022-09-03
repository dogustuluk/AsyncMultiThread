using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetContentAsync(CancellationToken token)
        {
            try
            {
                _logger.LogInformation("İstek başladı");

                await Task.Delay(2000, token);

                /*asenkron metot kullanmıyorsak
                token.ThrowIfCancellationRequested();
                */

                var mytask = new HttpClient().GetStringAsync("https://www.google.com");

                var data = await mytask;

                //--

                //Enumerable.Range(1, 10).ToList().ForEach(x =>
                // {
                //     Thread.Sleep(2000);
                //     token.ThrowIfCancellationRequested();
                // });


                _logger.LogInformation("İstek bitti");

                //return Ok("İşler Bitti");
                return Ok(data);

            }
            catch (Exception ex )
            {
                _logger.LogInformation("İstek İptal Edildi: "+ ex.Message);
                return BadRequest();
            }

            
        }


    }
}
