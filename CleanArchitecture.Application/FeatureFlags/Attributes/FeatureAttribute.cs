namespace CleanArchitecture.Application.FeatureFlags;

[AttributeUsage(AttributeTargets.Class)]
public class FeatureAttribute(
        string name,
        string description = "") 
    : Attribute
{
    public string Name = name;
    public string Description = description;
}