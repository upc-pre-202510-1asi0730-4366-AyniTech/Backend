namespace Lot.Inventaries.Domain.Model.ValueOjbects;

public record Category(string Name)
{
    public Category() : this(string.Empty)
    {
    }


}