using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.IRepository
{
    public interface IPantherRepository
    {
        IQueryable<PantherProfile> GetAll();
        PantherProfile GetById(int id);
        void Add(PantherProfile pantherProfile);
        void Update(PantherProfile pantherProfile);
        void Delete(int id);
        IQueryable<PantherType> GetPantherTypes();
        int GetTotalCount();
    }
}
