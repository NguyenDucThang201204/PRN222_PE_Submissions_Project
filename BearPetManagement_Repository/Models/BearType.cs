using BearPetManagement_Repository.NewFolder;
using System.Text.Json.Serialization;

namespace BearPetManagement_Repository.Models
{
    public class BearType
    {
        public int BearTypeId { get; set; }
        public string BearTypeName { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual List<BearProfile> BearProfiles { get; set; }
    }
}
