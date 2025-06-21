namespace Lot.IAM.Domain.Model.Commands
{
    public record SignUpCommand(string Name,string Email, string Password,string CardNumber, DateTime ExpiryDate, string CVV );
}

