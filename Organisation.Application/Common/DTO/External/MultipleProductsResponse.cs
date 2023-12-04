

namespace Organisation.Application.Common.DTO.External;

public sealed class MultipleProductsResponse
{
    public IEnumerable<ProductResponse> Products { get; set; }
}
