using Organisation.Application.Common.DTO;
using Organisation.Application.Common.Utilities;
using Swashbuckle.AspNetCore.Filters;

namespace Organisation.Presentation.API.Swagger.Examples.Responses;

public sealed class PagedCompanyResponseExample : IExamplesProvider<PageList<CompanyResponse>>
{
    public PageList<CompanyResponse> GetExamples()
    {
        var exampleCompanyResponses = new List<CompanyResponse>() {
          new CompanyResponse("name1","address1","country1"),
          new CompanyResponse("name2", "address2", "country2")
        };

        return PageList<CompanyResponse>.Create(exampleCompanyResponses, 1, 100, 1000);
    }
}
