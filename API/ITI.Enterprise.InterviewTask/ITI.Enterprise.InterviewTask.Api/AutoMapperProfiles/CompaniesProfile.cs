using AutoMapper;
using ITI.Enterprise.InterviewTask.DomainClasses;

namespace ITI.Enterprise.InterviewTask.Api.AutoMapperProfiles
{
    public class CompaniesProfile : Profile
    {
        public CompaniesProfile()
        {
            CreateMap<Company, DTO.CompanyDto>();


            CreateMap<DTO.CompanyDto, Company>();

        }
    }
}
