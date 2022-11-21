using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PolymorphismMethodOverriding.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public string? Post([FromBody] JsonDocument request, [FromQuery] int type)
        {
            var result = "";
            switch (type)
            {
                case 1:
                    result = request.Deserialize<Westerner>().ToString();
                    break;
                case 2:
                    result = request.Deserialize<Japanese>().ToString();
                    break;
                default:
                    result = request.Deserialize<NameClass>().ToString();
                    break;
            }
            return result;
        }
    }
}
public class NameClass
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

}

public class Westerner : NameClass
{
    public string MiddleName { get; set; }

    public override string? ToString()
        => new StringBuilder(FirstName)
        .Append(MiddleName)
        .Append(LastName).ToString();

}

public class Japanese : NameClass
{
    public override string? ToString()
        => new StringBuilder(LastName)
        .Append(FirstName).ToString();
}
