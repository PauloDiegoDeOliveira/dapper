using System.Security.Claims;

namespace Empresa.Dapper.Domain.Core.Interfaces.Service
{
    public interface IUser
    {
        string Name { get; }

        Guid GetUserId();

        string GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim> GetClaimsIdentity();

        string GetUserRole();

        IEnumerable<string> GetUserClaims();
    }
}