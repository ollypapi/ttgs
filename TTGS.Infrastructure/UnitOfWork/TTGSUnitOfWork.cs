using System;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;
using TTGS.Infrastructure.EF;
using TTGS.Infrastructure.EF.Data;
using TTGS.Infrastructure.Repository;
using TTGS.Shared.DTOs;
using TTGS.Shared.Entity;

namespace TTGS.Infrastructure.UnitOfWork
{
    public class TTGSUnitOfWork : ITTGSUnitOfWork
    {
        private TTGSContext _ttgsContext;
        private ApplicationDbContext _applicationDbContext;
        public TTGSUnitOfWork(TTGSContext microFinanceContext, ApplicationDbContext applicationDbContext)
        {
            _ttgsContext = microFinanceContext;
            _applicationDbContext = applicationDbContext;
        }

        private IGenericRepository<AspNetUsers> _aspNetUsers;
        public IGenericRepository<AspNetUsers> AspNetUsers => _aspNetUsers ?? (_aspNetUsers = new GenericRepository<AspNetUsers>(_applicationDbContext));

        private IGenericRepository<RoleDto> _roles;
        public IGenericRepository<RoleDto> Roles => _roles ?? (_roles = new GenericRepository<RoleDto>(_applicationDbContext));

        private IGenericRepository<UserRoleDto> _userRoles;
        public IGenericRepository<UserRoleDto> UserRoles => _userRoles ?? (_userRoles = new GenericRepository<UserRoleDto>(_applicationDbContext));

        private IGenericRepository<RefreshToken> _refreshTokens;
        public IGenericRepository<RefreshToken> RefreshTokens => _refreshTokens ?? (_refreshTokens = new GenericRepository<RefreshToken>(_applicationDbContext));

        private IGenericRepository<Communication> _communications;
        public IGenericRepository<Communication> Communications => _communications ?? (_communications = new GenericRepository<Communication>(_ttgsContext));

        private IGenericRepository<Order> _orders;
        public IGenericRepository<Order> Orders => _orders ?? (_orders = new GenericRepository<Order>(_ttgsContext));

        public void Save()
        {
            _applicationDbContext.SaveChanges();
            _ttgsContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
            await _ttgsContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _ttgsContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
