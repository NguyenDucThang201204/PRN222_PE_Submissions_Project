using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_NguyenNMK_DataAccessLayer.DTOs
{
    public class BearDTO
    {
        [Key]
        public int? BearProfileId { get; set; }

        public int? BearTypeId { get; set; }

        public string BearName { get; set; }

        public double BearWeight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime ModifiedDate { get; set; }
        public string BearTypeName { get; set; }

        public string Origin { get; set; }

        public string Description { get; set; }
    }
}
