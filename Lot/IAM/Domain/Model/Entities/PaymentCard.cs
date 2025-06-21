using System.Text.RegularExpressions;
using Lot.IAM.Domain.Model.Aggregates;

namespace Lot.IAM.Domain.Model.Entities
{
public class PaymentCard
{
    public int Id { get; private set; }
    public string CardNumber { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public string CVV { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }

    public PaymentCard()
    {
        CardNumber = string.Empty;
        CVV = string.Empty;
    }

    public PaymentCard(string cardNumber, DateTime expiryDate, string cvv/* , int userId */)
    {
        if (string.IsNullOrWhiteSpace(cardNumber))
            throw new ArgumentException("Card number cannot be null or empty.");

        if (!Regex.IsMatch(cardNumber, @"^\d{16}$"))
            throw new ArgumentException("Card number must be 16 digits.");

        if (string.IsNullOrWhiteSpace(cvv))
            throw new ArgumentException("CVV cannot be null or empty.");

        if (!Regex.IsMatch(cvv, @"^\d{3}$"))
            throw new ArgumentException("CVV must be 3 digits.");

        /* if (userId <= 0)
            throw new ArgumentException("User ID must be greater than 0."); */

        CardNumber = cardNumber;
        ExpiryDate = expiryDate;
        CVV = cvv;
        /* UserId = userId; */
    }
}
}

