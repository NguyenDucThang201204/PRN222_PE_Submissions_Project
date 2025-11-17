using Data.Entities;
using System.Collections.Generic;

namespace Data.Interfaces
{
    public interface IBearProfileRepository
    {
        IEnumerable<BearProfile> GetAll();
        BearProfile? GetById(int id);
        void Add(BearProfile bear);
        void Update(BearProfile bear);
        void Delete(BearProfile bear);
        void Save();
    }
}
