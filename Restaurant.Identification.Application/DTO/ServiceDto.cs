namespace Restaurant.Identification.Application.DTO;

public class ServiceDto
{

    public string? Id { get; set; }

    public string? Name { get; set; }

    public string? Secret { get; set; }

    public string[] Audiences { get; set; } = [];

    public string[] Roles { get; set; } = [];

}
