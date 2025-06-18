namespace Lot.Inventaries.Domain.Model.ValueOjbects;

public record Supplier(string Name)
{
    public Supplier() : this(string.Empty)
    {
    }

 
}