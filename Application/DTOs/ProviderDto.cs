namespace SampleCrud.Application.DTOs;

public class ProviderDto
{
    public int ProviderId { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateProviderDto
{
    public string Name { get; set; } = "";
}

public class UpdateProviderDto
{
    public string Name { get; set; } = "";
}