using Microsoft.AspNetCore.Mvc;

namespace UserApi.Controllers
{
    [Route("health")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public string CheckHealth()
        {
            return "healthy";
        }
    }
}
