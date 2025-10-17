using MediatR;
using Nsp.Common;

namespace IdentityServer.Application.Company.Commands.AddCompeny
{
    public class AddCompanyCommand : IRequest<Result<bool>>
    {
        public string? CeoCell { get; set; }
        public string? CeoFirstName { get; set; }

        public string? CeoLastName { get; set; }

        public string? RegisterNo { get; set; }

        public string? TellPhone { get; set; }

        public string? EconomicCode { get; set; }

        public string? NationalCode { get; set; }
        public DateTime? CEOBirthday { get; set; }
        public string? Address { get; set; }
        public string Name { get; set; }
    }
}
