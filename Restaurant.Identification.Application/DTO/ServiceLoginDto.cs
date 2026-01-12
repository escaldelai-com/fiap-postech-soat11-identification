namespace Restaurant.Identification.Application.DTO;

// Necessário para manter o padrão com o OAuth2
#pragma warning disable CA1707
#pragma warning disable IDE1006

public class ServiceLoginDto
{

    public string? grant_type { get; set; }

    public string? client_id { get; set; }

    public string? client_secret { get; set; }

}

#pragma warning restore IDE1006
#pragma warning restore CA1707