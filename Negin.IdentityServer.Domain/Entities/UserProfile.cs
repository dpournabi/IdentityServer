namespace IdentityServer.Domain.Entities;

public class UserProfile: BaseEntity<int>
{
    public  string FirstName { get; set; }
    public  string LastName { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? OfficeTell { get; set; }
    public string? OfficeFax { get; set; }
    public string? OfficeAddress { get; set; }
    public string? HomeTell { get; set; }
    public string? HomeAddress { get; set; }
    public string? CellPhone { get; set; }

    public  Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}
