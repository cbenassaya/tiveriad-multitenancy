using AutoMapper;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Multitenancy.Api.Contracts;

namespace Tiveriad.Multitenancy.Api.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReaderModel>();
        CreateMap<UserWriterModel, User>();
    }
}