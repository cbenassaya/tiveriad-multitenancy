using FluentValidation;
using Tiveriad.Multitenancy.Core.Entities;
using Tiveriad.Repositories;

namespace Tiveriad.Multitenancy.Applications.Commands.ClientCommands;
public class DeleteClientByIdPreValidator : AbstractValidator<DeleteClientByIdRequest>
{
    private IRepository<Client, string> _clientRepository;
    public DeleteClientByIdPreValidator(IRepository<Client, string> clientRepository)
    {
        _clientRepository = clientRepository;
    }
}