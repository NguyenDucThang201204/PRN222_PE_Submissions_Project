using PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.Implementations;

public sealed class BearAccountRepository : PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.BaseRepository<BearAccount, FA25BearDBContext>, IBearAccountRepository
{
    public BearAccountRepository(PE_PRN232_FA25_TranNguyenVietQuang_BE_Repository.IDbFactory<FA25BearDBContext> dbFactory)
        : base(dbFactory)
    {
    }

    public Task<BearAccount?> GetByUserNameAsync(string? userName)
        => DbSet.SingleOrDefaultAsync(x => x.Email == userName);
}