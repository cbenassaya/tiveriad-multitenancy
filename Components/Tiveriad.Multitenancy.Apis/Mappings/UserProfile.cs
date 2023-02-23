using AutoMapper;
using Tiveriad.Multitenancy.Api.Contracts;
using Tiveriad.Multitenancy.Core.Entities;

namespace Tiveriad.Multitenancy.Api.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReaderModel>();
        CreateMap<UserWriterModel, User>();
    }
}