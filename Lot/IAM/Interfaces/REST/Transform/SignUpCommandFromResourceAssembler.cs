using System.Globalization;
using Lot.IAM.Domain.Model.Commands;
using Lot.IAM.Interfaces.REST.Resources;

namespace Lot.IAM.Interfaces.REST.Transform
{
    public class SignUpCommandFromResourceAssembler
    {
        public static SignUpCommand ToCommandFromResource(SignUpResource resource)
        {
            if (!DateTime.TryParseExact(
                    resource.ExpiryDate,
                    "MM-yy",
                    CultureInfo.InvariantCulture, 
                    DateTimeStyles.None,
                    out var parsedDate))
                throw new ArgumentException("Invalid date format. Valid format is MM-yy");
            
            return new SignUpCommand(resource.Name, resource.Email, resource.Password, 
                resource.CardNumber, parsedDate, resource.CVV);
        }
    }
}

