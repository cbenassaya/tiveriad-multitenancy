using AutoMapper;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Api.Contracts;

namespace Tiveriad.Multitenancy.Api.Mappings;
public class MembershipProfile : Profile
{
    public MembershipProfile()
    {
        CreateMap<Membership, MembershipReaderModel>();
        CreateMap<MembershipWriterModel, Membership>();
    }
}