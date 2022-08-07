using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskWebApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetContentAsync()
        {
            //Bu api ile çalışan thread herhangi bir şekilde bloklanmış olmuyor. Eğer ilgili thread burada çalışmasına devam ederken bir başka request'i de handle edebilecek durumdadır.

            var mytask = new HttpClient().GetStringAsync("https://www.google.com"); //await koymadık yani metot çalıştığı zaman bu istek başlatılacak fakat herhangi bir sonuç almıyor olucaz.

            //Bu arada yapmak istediğimiz başka işlemler var ise yapabiliriz.

            var data = await mytask; //şimdi yukarıda isteğini başlatmış olduğumuz data'yı aldık.

            return Ok(data); 
        }


    }
}
