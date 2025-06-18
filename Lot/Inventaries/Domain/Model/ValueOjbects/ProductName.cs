namespace Lot.Inventaries.Domain.Model.ValueOjbects; 

public record ProductName(string Name)
{
    public ProductName() : this(string.Empty)
    {
    }


}