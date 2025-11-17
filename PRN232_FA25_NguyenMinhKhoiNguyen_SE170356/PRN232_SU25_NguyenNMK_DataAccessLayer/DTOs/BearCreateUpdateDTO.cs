using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_DataAccessLayer.DTOs
{
    public class BearCreateUpdateDTO
    {
        public int? BearProfileId { get; set; }
        public string BearName { get; set; }
        public double BearWeight { get; set; }
        public string Characteristics { get; set; }
        public string CareNeeds { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int BearTypeId { get; set; }
    }
}
