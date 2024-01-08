using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArchitecture.Api.Endpoints.Extensions;

internal static class MapEndpointsExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        // get all the types that implement BaseEndpoint
        var endpointGroupTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(BaseEndpointGroup)) && !t.IsAbstract)
            .ToImmutableArray();

        foreach (var endpointGroupType in endpointGroupTypes)
        {
            // get the group name from the BaseEndpoint
            var groupName = endpointGroupType.BaseType
                ?.GetProperties()
                .First(x => x.Name == "GroupName")
                .GetValue(Activator.CreateInstance(endpointGroupType))
                ?.ToString() ?? string.Empty;

            // get all the methods that have an Http attribute
            var methods = endpointGroupType
                .GetMethods()
                .Where(m => m is {IsPublic: true})
                .Where(m => m
                    .GetCustomAttributes()
                    .Any(a => a.GetType().Name.StartsWith("Http")))
                .ToArray();

            foreach (var method in methods)
            {
                var httpAttribute = method.GetCustomAttributes()
                    .First(a => a.GetType().Name.StartsWith("Http"));

                var verb = httpAttribute.GetType().Name
                    .Replace("Attribute", "")
                    .Replace("Http", "")
                    .ToUpper();

                var route = httpAttribute.GetType()
                    .GetProperties()
                    .First(x => x.Name == "Template")
                    .GetValue(httpAttribute)
                    ?.ToString() ?? string.Empty;
                

                var methodDelegate = method.CreateDelegate(
                    Expression.GetFuncType(
                        method.GetParameters()
                            .Select(p => p.ParameterType)
                            .Concat(new[] {method.ReturnType})
                            .ToArray()
                    )
                );
                
                var summaryAttribute = method.GetCustomAttributes()
                    .FirstOrDefault(a => a.GetType().Name == "EndpointSummaryAttribute");
                
                var summary = summaryAttribute?.GetType()
                    .GetProperties()
                    .First(x => x.Name == "Summary")
                    .GetValue(summaryAttribute)
                    ?.ToString() ?? string.Empty;

                // ReSharper disable once RouteTemplates.SyntaxError
                app
                    .MapMethods($"/{groupName}/{route}", new[] {verb}, methodDelegate)
                    .WithOpenApi(operation =>
                    {
                        operation.Summary = summary;

                        return operation;
                    });
            }
        }

        return app;
    }
}