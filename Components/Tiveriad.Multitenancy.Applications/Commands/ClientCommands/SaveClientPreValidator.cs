using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.ClientCommands;
public class SaveClientPreValidator : AbstractValidator<SaveClientRequest>
{
    private IRepository<Client, string> _clientRepository;
    public SaveClientPreValidator(IRepository<Client, string> clientRepository)
    {
        _clientRepository = clientRepository;
    }
}