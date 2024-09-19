using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace UMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private IOptions<RegLogfileConfig> Config;
        public LogController(IOptions<RegLogfileConfig> config)
        {
            Config = config;
        }
        [HttpGet("LogData")]
        public string GetRegLog()
        {
            string logfilepath = Config.Value.RegLogFilePath;
            string path = Path.Combine(logfilepath, "Reglog.txt");
            FileStream fileStream = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))

                return reader.ReadToEnd();
        }
    }
}

                

     