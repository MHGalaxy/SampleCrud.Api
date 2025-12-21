namespace SampleCrud.Domain.Entities;

public class Provider
{
    public int ProviderId { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}