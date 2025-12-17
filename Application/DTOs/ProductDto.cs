namespace SampleCrud.Application.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public decimal Price { get; set; }
    public string? ImageSrc { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class CreateProductDto
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public decimal Price { get; set; }
    public string? ImageSrc { get; set; }
}

public class UpdateProductDto
{
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public int ProviderId { get; set; }
    public decimal Price { get; set; }
    public string? ImageSrc { get; set; }
}