namespace Organisation.Application.Common.DTO.External;

public sealed class ProductResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double DiscountPercentage { get; set; }
    public double Rating { get; set; }
    public int Stock { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public string Thumbnail { get; set; }
    public string[] Images { get; set; }
}
