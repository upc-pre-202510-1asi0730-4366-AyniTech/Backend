namespace Lot.IAM.Interfaces.REST.Resources
{
    public record SignUpResource(string Name, string Email, string Password,string CardNumber, string ExpiryDate, string CVV);
}

