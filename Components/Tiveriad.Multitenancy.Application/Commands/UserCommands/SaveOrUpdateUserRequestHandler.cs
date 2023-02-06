using Microsoft.EntityFrameworkCore;
using MediatR;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace Tiveriad.Multitenancy.Application.Commands.UserCommands;
public class SaveOrUpdateUserRequestHandler : IRequestHandler<SaveOrUpdateUserRequest, User>
{
    private readonly IRepository<User, string> _userRepository;
    public SaveOrUpdateUserRequestHandler(IRepository<User, string> userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> Handle(SaveOrUpdateUserRequest request, CancellationToken cancellationToken)
    {
        var query = _userRepository.Queryable.Where(x => x.Id == request.User.Id);
        return Task.Run(async () =>
        {
            //<-- START CUSTOM CODE-->
            var result = query.ToList().FirstOrDefault();
            if (result == null)
            {
                await _userRepository.AddOneAsync(request.User, cancellationToken);
                return request.User;
            }
            else
            {
                result.Email = request.User.Email;
                result.Firstname = request.User.Firstname;
                result.Lastname = request.User.Lastname;
                result.Description = request.User.Description;
                result.State = request.User.State;
                result.CreatedBy = request.User.CreatedBy;
                result.Created = request.User.Created;
                result.LastModifiedBy = request.User.LastModifiedBy;
                result.LastModified = request.User.LastModified;
                return result;
            }
        //<-- END CUSTOM CODE-->
        }, cancellationToken);
    }
}