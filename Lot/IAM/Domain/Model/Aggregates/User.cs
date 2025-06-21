using Lot.IAM.Domain.Model.Commands;

namespace Lot.IAM.Domain.Model.Aggregates
{
    public class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public User()
        {
            Name = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
        }

        public User(SignInCommand command)
        {
            Email = command.Email;
            Password = command.Password;
        }

        public User(SignUpCommand command)
        {
            LastName = command.LastName;
            Name = command.Name;
            Email = command.Email;
            Password = command.Password;
        } 
    }
}

