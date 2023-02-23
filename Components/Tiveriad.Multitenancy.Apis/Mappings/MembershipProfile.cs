using AutoMapper;
using Tiveriad.Multitenancy.Api.Contracts;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Mappings;
public class MembershipProfile : Profile
{
    public MembershipProfile()
    {
        CreateMap<Membership, MembershipReaderModel>();
        CreateMap<MembershipWriterModel, Membership>();
    }
}