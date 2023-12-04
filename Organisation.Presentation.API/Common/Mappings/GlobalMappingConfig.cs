using Mapster;
using Organisation.Application.Common.DTO;
using Organisation.Application.CompanyModule.Commands.UpdateCompany;
using Organisation.Application.EmployeeModule.Commands.AddEmployee;
using Organisation.Application.UserModule.Commands.RegisterUser;

namespace Organisation.Presentation.API.Common.Mappings;

public sealed class GlobalMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(string Id, CompanyRequest CompanyRequest), UpdateCompanyCommand>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest, src => src.CompanyRequest);

        config.NewConfig<CreateEmployeeRequest, AddEmployeeCommand>()
              .Map(dest => dest.CreateEmployeeRequest, src => src);
        config.NewConfig<RegisterUserRequest, RegisterUserCommand>()
              .Map(dest => dest.Command, src => src);
    }
}
