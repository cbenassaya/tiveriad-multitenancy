using AutoMapper;
using Tiveriad.Multitenancy.Api.Contracts;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Mappings;
public class OrganizationProfile : Profile
{
    public OrganizationProfile()
    {
        CreateMap<Organization, OrganizationReaderModel>();
        CreateMap<OrganizationWriterModel, Organization>();
    }
}