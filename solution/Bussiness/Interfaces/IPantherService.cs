using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Interfaces
{
    public interface IPantherService
    {
        IEnumerable<PantherViewModel> GetPantherProfiles(int pageNumber, int pageSize);
        PantherProfile GetPantherProfileById(int id);
        void AddPantherProfile(PantherProfile pantherProfile);
        void UpdatePantherProfile(PantherProfile pantherProfile);
        void DeletePantherProfile(int id);
        IEnumerable<PantherType> GetPantherTypes();
        int GetTotalCount();
    }

    public class PantherViewModel
    {
        public int Id { get; set; }
        public string PantherName { get; set; }
        public double Weight { get; set; }
        public string PantherTypeName { get; set; }
    }
}
