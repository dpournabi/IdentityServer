using IdentityServer.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nsp.Common;

namespace IdentityServer.Application.User.Queries.AllUsers
{
    public class GetAllUsersQuery : IRequest<Result<IEnumerable<GetAllUsersResponse>>>
    {
        public string Phrase { get; set; }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<GetAllUsersResponse>>>
    {
        private readonly IApplicationDbContext _dbContext;
        public GetAllUsersQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<IEnumerable<GetAllUsersResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<GetAllUsersResponse> result =
            await (from u in _dbContext.ApplicationUsers
                   join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                   join r in _dbContext.ApplicationRoles on ur.RoleId equals r.Id
                   where
                         r.Name != "Root" &&
                         //r.Name != AssessorsManager.Administrator &&
                         (request.Phrase == null ||
                             request.Phrase != null && (
                                 u.FirstName.Contains(request.Phrase) ||
                                 u.LastName.Contains(request.Phrase) ||
                                 u.UserName.Contains(request.Phrase) ||
                                 r.Name.Contains(request.Phrase) ||
                                 r.LocalName.Contains(request.Phrase)
                             )
                          )
                   select new GetAllUsersResponse
                   {
                       Id = u.Id,
                       UserName = u.UserName,
                       Firstname = u.FirstName,
                       Lastname = u.LastName,
                       PhoneNumber = u.PhoneNumber,
                       Roles = r.LocalName,
                       IsActive = u.IsActive
                   }
                                  ).ToListAsync();

            return Result<IEnumerable<GetAllUsersResponse>>.Success(result.GroupBy(u => u.UserName).Select(u => new GetAllUsersResponse()
            {
                Id = u.First().Id,
                Firstname = u.First().Firstname,
                Lastname = u.First().Lastname,
                UserName = u.Key,
                PhoneNumber = u.First().PhoneNumber,
                Roles = string.Join(", ", u.Select(g => g.Roles).ToArray()),
                IsActive = u.First().IsActive
            }).ToList());
        }
    }
}
