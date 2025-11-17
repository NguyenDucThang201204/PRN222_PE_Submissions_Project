using PE_PRN232_FA25_LeHoangTrong_BE_repo.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

namespace PE_PRN232_FA25_LeHoangTrong_BE_repo.Implementations;

public sealed class BearTypeRepository : PE_PRN232_FA25_LeHoangTrong_BE_repo.BaseRepository<BearType, FA25BearDB>, IBearTypeRepository
{
    public BearTypeRepository(PE_PRN232_FA25_LeHoangTrong_BE_repo.IDbFactory<FA25BearDB> dbFactory)
        : base(dbFactory)
    {
    }
}