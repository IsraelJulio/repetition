using Domain.Entities;
using Microsoft.AspNetCore.OData.Query.Expressions;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.UriParser;
using System;
using System.Linq.Expressions;

namespace Api.Extensions.OData
{
    public static class SearchBindingsConfiguration
    {
        public static IServiceCollection GetSearchBindings(this IServiceCollection services)
        {
            ODataValidationSettings validationSettingFactory(IServiceProvider sp) => new ODataValidationSettings
            {
                MaxAnyAllExpressionDepth = 4,
                MaxExpansionDepth = 4
            };

            services.AddSingleton(validationSettingFactory);
            services.AddSingleton<ISearchBinder, CustomSearchBinder>();
            return services;
        }
    }

    #region SearchBinderByEntity

    public class CustomSearchBinder : QueryBinder, ISearchBinder
    {
        public Expression BindSearch(SearchClause searchClause, QueryBinderContext context)
        {
            SearchTermNode node = searchClause.Expression as SearchTermNode;
            string searchedTerm = node.Text.ToLower();

            string entity = context.ElementClrType.Name;

            if (entity == "Quiz")
            {
                Expression<Func<Quiz, bool>> exp = x =>
                           x.Description.ToLower().Contains(searchedTerm)
                        || x.Title.ToLower().Contains(searchedTerm);
                return exp;
            }
            
            return null;
        }
    }

    #endregion SearchBinderByEntity
}