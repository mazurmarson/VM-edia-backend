using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using VM_ediaAPI.Data;
using VM_ediaAPI.Dtos;

namespace VM_ediaAPI.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<JsonPatchDocument<UpdateUserDto>>
    {

        public UpdateUserDtoValidator(DataContext dataContext)
        {


            RuleFor(x => x.Operations).Custom((values, context) =>
            {
                var NotValidPathValue = values.Where(x => x.path != "/mail" && x.path != "/description" && x.path != "/mainPhotoUrl").Any();
                if (NotValidPathValue)
                {
                    context.AddFailure("Wrong property", "Nie ma takiej właściwości");
                }

            });

            RuleFor(x => x.Operations).Custom((values, context) =>
            {
            var mailValidator = new EmailAddressAttribute();
            foreach (var value in values)
            {
                if (value.path == "/mail")
                {
                    string validationEmail = value.value.ToString();
                    if(!mailValidator.IsValid(validationEmail))
                    {
                        context.AddFailure("/mail", "Wprowadzony adres nie jest adresem e-mail");
                    }
                }
            }
            }
            );

            RuleFor(x => x.Operations).Custom((values, context) => {
                foreach(var value in values)
                {
                    if(value.value.ToString().Length > 250)
                    {
                        context.AddFailure("description", "Wprowadzony opis jest dłuższy niż 250 znaków");
                    }
                }
            });

        }
    }
}