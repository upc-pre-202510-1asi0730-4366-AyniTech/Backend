namespace Lot.IAM.Interfaces.REST.Resources
{
    public record AuthenticatedUserResource(int Id, string Name, string LastName, string Token);
}

