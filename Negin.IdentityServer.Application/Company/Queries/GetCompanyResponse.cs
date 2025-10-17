namespace IdentityServer.Application.Company.Queries;

public record GetCompanyResponse(int Id, string RegisterNo, string Name,
    string? TellPhone, string? CEOCell, string? CEOFirstName, string? CEOLastName, string? EconomicCode, string? NationalCode, DateTime? CEOBirthday, string? Address)
{
    public static implicit operator GetCompanyResponse(Domain.Entities.Company company)
    {
        return new GetCompanyResponse(company.Id, company.RegisterNo, company.Name, company?.TellPhone,
            company?.CEOCell, company?.CEOFirstName, company?.CEOLastName, company?.EconomicCode, company?.NationalCode, company?.CEOBirthday, company?.Address);
    }
}
