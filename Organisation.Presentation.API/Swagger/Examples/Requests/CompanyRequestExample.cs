using Organisation.Application.Common.DTO;
using Swashbuckle.AspNetCore.Filters;

namespace Organisation.Presentation.API.Swagger.Examples.Requests;

public sealed class CompanyRequestExample : IExamplesProvider<CompanyRequest>
{
    public CompanyRequest GetExamples()
    {
        return new CompanyRequest("name of the company", "address of the company", "country og the company");
    }
}