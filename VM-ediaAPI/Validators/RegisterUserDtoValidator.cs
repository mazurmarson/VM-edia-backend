using System.Linq;
using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Login).NotEmpty().MaximumLength(40).MinimumLength(3).Custom((value, context) => {
               var loginInUse = dataContext.Users.Any(x => x.Login == value);
               if(loginInUse)
               {
                   context.AddFailure("Login", "Podany mail jest w użyciu");
               }
            });

            RuleFor(x => x.Mail).NotEmpty().MaximumLength(60).EmailAddress().Custom((value, context) => {
                var mailInUse = dataContext.Users.Any(x => x.Mail == value);
                if(mailInUse)
                {
                    context.AddFailure("Mail", "Podany mail jest w użyciu");
                }
            });

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).MaximumLength(50);

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password);

            RuleFor(x => x.Description).MaximumLength(250);
        }
    }
}