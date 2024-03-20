using Domain.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;

namespace Api.Extensions.OData
{
    public static class EdmModelConfiguration
    {
        public static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();
            builder.EnableLowerCamelCase();

            builder.EntitySet<Quiz>(nameof(Quiz)).EntityType
                .Select().OrderBy().Filter().Page().Count();

            return builder.GetEdmModel();
        }
    }
}