using BO;
using DAL;

namespace Repo
{
    public class BearAccountRepo
    {
        private readonly BearAccountDAO _dao;
        public BearAccountRepo(BearAccountDAO dao)
        {
            _dao = dao;
        }
        public BearAccount Login(string username,string password)
        {
            return _dao.Login(username, password);
        }
    }
}
