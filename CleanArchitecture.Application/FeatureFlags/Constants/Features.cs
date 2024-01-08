namespace CleanArchitecture.Application.FeatureFlags;

public static class Features
{
    public static Feature RecipesFeature { get; } = new(
        "Recipes",
        "Feature for creating and managing recipes",
        true,
        new[]
        {
            new Feature(
                "Get",
                "Feature for getting recipes",
                true),
            new Feature(
                "Create",
                "Feature for creating recipes",
                true),
            new Feature(
                "Update",
                "Feature for updating recipes",
                true),
            new Feature(
                "Delete",
                "Feature for deleting recipes",
                true)
        });
}

public sealed record Feature(
        string Name,
        string Description,
        bool Enabled,
        IEnumerable<Feature> SubFeatures = default!);

internal static class RecipesFeature
{
    public abstract class RecipesFeatureAttribute(string subFeatureName)
        : FeatureAttribute(Features.RecipesFeature.Name, "Feature for creating and managing recipes")
    {
        public string SubFeatureName { get; } = subFeatureName;
    }

    public sealed class RecipeGetFeatureAttribute() : RecipesFeatureAttribute("Get");
    public sealed class RecipeCreateFeatureAttribute() : RecipesFeatureAttribute("Create");
    public sealed class RecipeUpdateFeatureAttribute() : RecipesFeatureAttribute("Update");
    public sealed class RecipeDeleteFeatureAttribute() : RecipesFeatureAttribute("Delete");
}


// return $@"// <auto-generated />
// #pragma warning disable 1591
//
// namespace {AttributeNamespace} 
// {{
//     [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
//     internal sealed class {attributeName} : Attribute
//     {{
//         public string SubFeatureName {{ get; }}
//         public {AttributeNameSuffix}(string subFeatureName) 
//         {{
//             SubFeatureName = subFeatureName;
//         }}
//     }}
// }}";