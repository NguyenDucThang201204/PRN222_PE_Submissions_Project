using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using Repository.Models;

namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Configurations
{
    public static class ODataConfig
    {
        public static IEdmModel GetEDModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<BearProfile>("BearProfile");
            builder.EntitySet<BearType>("BearType");
             

            return builder.GetEdmModel();
        }
    }
}
