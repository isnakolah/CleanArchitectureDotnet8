namespace CleanArchitecture.Api.Endpoints.Common;

public abstract class EndpointGroup(string name) 
{
    public string GroupName { get; } = name;
}