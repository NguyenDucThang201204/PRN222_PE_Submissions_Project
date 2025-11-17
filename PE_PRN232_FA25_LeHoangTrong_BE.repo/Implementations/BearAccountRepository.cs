using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo.Implementations;

public sealed class BearAccountRepository : PE_PRN232_FA25_LeHoangTrong_BE_repo.BaseRepository<BearAccount, FA25BearDB>, IBearAccountRepository
{
    public BearAccountRepository(PE_PRN232_FA25_LeHoangTrong_BE_repo.IDbFactory<FA25BearDB> dbFactory)
        : base(dbFactory)
    {
    }

    public Task<BearAccount?> GetByEmailAsync(string? email)
        => DbSet.SingleOrDefaultAsync(x => x.Email == email);
}