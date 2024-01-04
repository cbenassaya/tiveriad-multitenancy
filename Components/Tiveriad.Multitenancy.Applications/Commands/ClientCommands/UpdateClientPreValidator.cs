using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.ClientCommands;

public class UpdateClientPreValidator : AbstractValidator<UpdateClientRequest>
{
    private IRepository<Client, string> _clientRepository;
    public UpdateClientPreValidator(IRepository<Client, string> clientRepository)
    {
        _clientRepository = clientRepository;
    }
}