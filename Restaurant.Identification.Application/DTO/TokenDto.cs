using System.Text.Json.Serialization;

namespace Restaurant.Identification.Application.DTO;

public class TokenDto
{

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    public double ExpiresIn { get; set; }

    [JsonPropertyName("refresh_token")]
    public string? RefreshToken { get; set; }

    [JsonPropertyName("token_type")]
    public string? TokenType { get; set; }


}
