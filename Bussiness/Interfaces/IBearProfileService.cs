using Data.Entities;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IBearProfileService
    {
        IEnumerable<BearProfile> GetAll();
        BearProfile? GetById(int id);
        void Add(BearProfile bear);
        void Update(int id, BearProfile bear);
        void Delete(int id);
    }
}
