namespace CleanArchitecture.Api.Endpoints.Common;

public abstract class BaseEndpointGroup(string name, string version) 
{
    public string GroupName { get; } = $"api/{version}/{name}";
}

public abstract class EndpointGroup(string name) : BaseEndpointGroup(name, "v1");

