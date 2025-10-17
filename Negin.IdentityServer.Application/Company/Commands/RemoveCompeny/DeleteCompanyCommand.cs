using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Company.Commands.RemoveCompeny
{
    public class DeleteCompanyCommand : IRequest<Result<bool>>
    {
        public int Id { get; set; }
    }
}
