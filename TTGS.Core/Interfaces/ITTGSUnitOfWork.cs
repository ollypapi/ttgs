using System;
using System.Threading.Tasks;
using TTGS.Shared.DTOs;
using TTGS.Shared.Entity;

namespace TTGS.Core.Interfaces
{
    public interface ITTGSUnitOfWork : IDisposable
    {
        IGenericRepository<AspNetUsers> AspNetUsers { get; }
        IGenericRepository<RoleDto> Roles { get; }
        IGenericRepository<UserRoleDto> UserRoles { get; }
        IGenericRepository<RefreshToken> RefreshTokens { get; }
        IGenericRepository<Communication> Communications { get; }
        IGenericRepository<Order> Orders { get; }
        Task SaveAsync();
        void Save();
    }
}