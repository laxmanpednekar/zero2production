

using Newtonsoft.Json;
using Organisation.Application.Common.DTO.External;

namespace Organisation.Application.ThirdPartyServices;

public sealed class ExternalProductsService
{
    private readonly HttpClient _client;
    public ExternalProductsService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<ProductResponse>> GetExternalProductsAsync()
    {
        var response = await _client.GetAsync("products");


        if (response.IsSuccessStatusCode)
            return JsonConvert.DeserializeObject<MultipleProductsResponse>(await response.Content.ReadAsStringAsync()).Products.ToList();
        else
            return null;
    }
}
