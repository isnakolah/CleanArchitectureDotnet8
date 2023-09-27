namespace CleanArchitecture.Application.Mappings;

public interface IViewModel<T>
{
    public int Id { get; set; }

    public void Mapping(Profile profile);
}