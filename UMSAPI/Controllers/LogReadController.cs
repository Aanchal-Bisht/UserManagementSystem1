using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace UMSAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogReadController : Controller
    {
       private IOptions<LogFileConfig> _logConfig;
        public LogReadController(IOptions<LogFileConfig> logConfig)
        {
            _logConfig = logConfig;
        }
        [HttpGet("LogRead")]
        public string Get()
        {
            string logFilePath = _logConfig.Value.LogFilePath;
            string path = Path.Combine(logFilePath, "log.txt");
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string line = reader.ReadToEnd();
                return line;
            }


        }

    }
}
