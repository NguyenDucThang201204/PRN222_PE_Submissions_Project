using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Repositories
{
    public interface IBearAccountRepository
    {
        Task<BearAccount> GetBearAccount(string email, string password);
    }
}
