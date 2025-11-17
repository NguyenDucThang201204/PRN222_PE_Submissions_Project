using Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ModelExtentions
{
    public class CreateRequest
    {
        public int LeopardProfileId { get; set; }

        public int LeopardTypeId { get; set; }
        public string LeopardName { get; set; }
        [Range(15.01, double.MaxValue, ErrorMessage = "Weight must be greater than 15")]
        public double Weight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime ModifiedDate { get; set; }

    }
}
