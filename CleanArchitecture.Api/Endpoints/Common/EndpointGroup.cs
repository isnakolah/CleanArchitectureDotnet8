namespace CleanArchitecture.Api.Endpoints.Common;

public abstract class EndpointGroup(string name, string version) 
{
    public string GroupName { get; } = $"api/{version}/{name}";
}

public abstract class V1EndpointGroup(string name) : EndpointGroup(name, "v1");

public abstract class V2EndpointGroup(string name) : EndpointGroup(name, "v2");