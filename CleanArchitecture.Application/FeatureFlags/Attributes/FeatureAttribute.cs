namespace CleanArchitecture.Application.FeatureFlags;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class FeatureAttribute(
        string name) 
    : Attribute
{
    public string Name = name;
}