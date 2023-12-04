
namespace Organisation.Application.Common.DTO;

public class RefreshToken
{
    public string TokenValue { get; set; }
    public DateTime Expires { get; set; }
}
