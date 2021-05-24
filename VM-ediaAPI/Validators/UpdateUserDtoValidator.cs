using System.Linq;
using FluentValidation;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator(DataContext dataContext)
        {
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Mail).NotEmpty().EmailAddress().MaximumLength(50).Custom((value, context) => {
                var mailInUse = dataContext.Users.Any(x => x.Mail == value);
                if(mailInUse)
                {
                    context.AddFailure("Mail", "Podany adres jest w użyciu");
                }
            });

            
        }
    }
}